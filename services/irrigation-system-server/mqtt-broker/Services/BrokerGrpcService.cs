using Grpc.Core;


namespace MQTTBroker;

public class BrokerGrpcService : BrokerGrpc.BrokerGrpcBase
{
    private readonly IMqttBroker _broker;
    public BrokerGrpcService(IMqttBroker mqttBroker)
    {
        _broker = mqttBroker;
    }

    public override Task<Empty> OpenValve(OpenValveRequest request, ServerCallContext context)
    {
        var topic = $"/home/irrigation-system/{request.Info.ClientId}/valves/{request.Info.ValvePin}";
        var payload = $"1:{request.Duration}";
        _broker.SendMessage(topic, payload);
        return Task.FromResult(new Empty());
    }
    
    public override Task<Empty> CloseValve(CloseValveRequest request, ServerCallContext context)
    {
        var topic = $"/home/irrigation-system/{request.Info.ClientId}/valves/{request.Info.ValvePin}";
        var payload = $"0";
        _broker.SendMessage(topic, payload);
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> ReadSensor(ReadSensorRequest request, ServerCallContext context)
    {
        var topic = $"/home/irrigation-system/{request.Info.ClientId}/moisture-sensors/{request.Info.SensorPin}";
        var payload = "1"; // TODO: Right Payload for command?
        _broker.SendMessage(topic, payload);
        return Task.FromResult(new Empty());
    }
}

