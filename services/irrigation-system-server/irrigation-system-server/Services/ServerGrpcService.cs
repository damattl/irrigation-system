using Grpc.Core;

namespace irrigation_system_server.Services;

public class ServerGrpcService : ServerGrpc.ServerGrpcBase
{
    public override Task<Empty> SendSensorData(SensorData request, ServerCallContext context) // TODO: Think about a better name
    {
        Console.WriteLine("ClientId: " + request.ClientId);
        Console.WriteLine("SensorId: " + request.SensorId);
        Console.WriteLine("SensorReading" + request.SensorReading);
        return Task.FromResult(new Empty());
    }
}