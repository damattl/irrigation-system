using MQTTnet;
using MQTTnet.Server;

namespace MQTTBroker;

public class MqttBroker : IMqttBroker
{
    private readonly MqttServer _mqttServer;
    
    public MqttBroker(MqttRouter router, MqttServerOptions? options)
    {
        var mqttFactory = new MqttFactory();
        _mqttServer = mqttFactory.CreateMqttServer(
            options ?? new MqttServerOptionsBuilder()
                .WithDefaultEndpoint()
                .Build()
            );

        _mqttServer.InterceptingInboundPacketAsync += router.HandleInbound;
        
    }

    public async void StartAsync()
    {
        await _mqttServer.StartAsync();
        
        Console.WriteLine("Press enter to exit"); // TODO: Find a better way to do this 
        Console.ReadLine();

        await _mqttServer.StopAsync();
    }

    public async void SendMessage(string topic, string payload)
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(topic) // "/home/irrigation-system/{clientId}/valves/1"
            .WithPayload(payload)
            .Build();
        var injectedApplicationMessage = new MqttInjectedApplicationMessage(message);
        Console.WriteLine("Sending Message");
        await _mqttServer.InjectApplicationMessage(injectedApplicationMessage);
    }
}

public interface IMqttBroker
{
    public void StartAsync();
    public void SendMessage(string topic, string payload);
}