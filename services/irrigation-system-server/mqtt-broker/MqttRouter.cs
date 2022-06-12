using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.HttpLogging;
using MQTTnet.Packets;
using MQTTnet.Server;

namespace MQTTBroker;


public class MqttRouter
{
    private ServerGrpc.ServerGrpcClient _client;
    public MqttRouter(ServerGrpc.ServerGrpcClient client)
    {
        _client = client;
    }
    public Task HandleInbound(InterceptingPacketEventArgs eventArgs)
    {
        if (eventArgs.Packet is MqttConnectPacket connectPacket)
        {
            Console.WriteLine(connectPacket.ClientId + " just connected!");
        }
        if (eventArgs.Packet is MqttPublishPacket packet)
        {
            Console.WriteLine(packet.Topic);
            var methods = typeof(MqttRouter)
                .GetMethods()
                .Where(x => x.GetCustomAttributes(typeof(MqttRouteAttribute), false).FirstOrDefault() != null);

            foreach (var method in methods)
            {
                var routeAttributes = method
                    .GetCustomAttributes(typeof(MqttRouteAttribute))
                    .Cast<MqttRouteAttribute>();
                var routeAttribute = routeAttributes.FirstOrDefault();
                if (routeAttribute == null)
                {
                    continue;
                }
                Console.WriteLine(routeAttribute.Regex);
                if (routeAttribute.Regex.IsMatch(packet.Topic))
                {
                    method.Invoke(this, new object[]{routeAttribute.Regex, eventArgs.ClientId, packet});
                }
                
            }
        }
        return Task.CompletedTask;
    }
    
    
    [MqttRoute(@"\/home\/irrigation-system\/([a-zA-z0-9-]+)\/moisture-sensors\/([0-9]+)")] // TODO: There should be a better way
    public async void HandleMoistureSensorMessage(Regex regex, string clientId, MqttPublishPacket packet)
    {
        var matches = regex.Matches(packet.Topic);
        var groups = matches[0].Groups;
        if (groups.Count < 3)
        {
            return;
        }

        var clientIdId = groups[3].Value;
        Console.WriteLine("ClientId: {0}", clientId);
        var sensorId = groups[2].Value;
        Console.WriteLine("SensorId: {0}", sensorId);
        var payload = System.Text.Encoding.Default.GetString(packet.Payload);
        var sensorReading = float.Parse(payload);
        Console.WriteLine("SensorReading: {0}" ,sensorReading);
        
        await _client.SendSensorDataAsync(
            new SensorData{ClientId = clientId, SensorPin = Int32.Parse(sensorId), SensorReading = sensorReading}
        );
    }
}

