using Grpc.Core;


namespace MQTTBroker;

public class BrokerGrpcService : BrokerGrpc.BrokerGrpcBase
{
    private readonly MqttBroker _broker;
    public BrokerGrpcService(MqttBroker mqttBroker)
    {
        _broker = mqttBroker;
    }

    public override Task<Empty> OpenValve(OpenValveRequest request, ServerCallContext context)
    {
        var topic = $"/home/irrigation-system/{request.Info.ClientId}/valves/{request.Info.ValveId}";
        var payload = $"1:{request.Duration}";
        _broker.SendMessage(topic, payload);
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> ReadSensor(SensorInfo request, ServerCallContext context)
    {
        var topic = $"/home/irrigation-system/{request.ClientId}/valves/{request.SensorId}";
        var payload = "1"; // TODO: Right Payload for command?
        _broker.SendMessage(topic, payload);
        return Task.FromResult(new Empty());
    }
}

