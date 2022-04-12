using MQTTBroker;
using MQTTnet;
using MQTTnet.Packets;
using MQTTnet.Server;

Task PrintPacket(InterceptingPacketEventArgs eventArgs)
{
    
    if (eventArgs.Packet is MqttPublishPacket packet)
    {
        HandlePublishPacket(eventArgs.ClientId, packet);
    }
    
    return Task.CompletedTask;
}


void HandlePublishPacket(string clientId, MqttPublishPacket packet) 
{
    Console.WriteLine(packet.Topic);
    var payload = System.Text.Encoding.Default.GetString(packet.Payload);
    Console.WriteLine(payload);
    
    if (packet.Topic == "/home/irrigation-system/moisture-sensors/1")
    {
        float value = float.Parse(payload);
        Console.WriteLine(payload);
    } 
}

void HandleMoistureSensorMessage((RouteHandler handler, string clientId, MqttPublishPacket packet) data)
{
    var matches = data.handler.Regex.Matches(data.packet.Topic);
    var groups = matches[0].Groups;
    if (groups.Count < 2)
    {
        return;
    }

    var sensorId = int.Parse(groups[1].Value);
    Console.WriteLine("SensorId: {0}", sensorId);
    var payload = System.Text.Encoding.Default.GetString(data.packet.Payload);
    var sensorReading = float.Parse(payload);
    Console.WriteLine("SensorReading: {0}" ,sensorReading);
}


async Task RunMinimalServer()
{
    var mqttFactory = new MqttFactory();
    
    var mqttServerOptions = new MqttServerOptionsBuilder()
        .WithDefaultEndpointPort(1884)
        .WithDefaultEndpoint()
        .Build();

    var router = new MQTTRouter();
    router.RegisterRoute(@"\/home\/irrigation-system\/moisture-sensors\/([0-9]+)", HandleMoistureSensorMessage);
    
    using var mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions);
    mqttServer.InterceptingInboundPacketAsync += router.HandleInbound;

    
    
    await mqttServer.StartAsync();
    Console.WriteLine("Press enter to exit");

    mqttServer.InterceptingInboundPacketAsync += async eventArgs =>
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic("/home/irrigation-system/valves/1")
            .WithPayload("Test")
            .Build();
        var injectedApplicationMessage = new MqttInjectedApplicationMessage(message);
        Console.WriteLine("Sending");
        await mqttServer.InjectApplicationMessage(injectedApplicationMessage);
    };

    mqttServer.InterceptingPublishAsync += eventArgs =>
    {
        Console.WriteLine(eventArgs.ApplicationMessage.Topic);
        return Task.CompletedTask;
    };
    
    Console.ReadLine();

    await mqttServer.StopAsync();
}

await RunMinimalServer();