using Grpc.Core;
using IrrigationSystemServer.Data;
using IrrigationSystemServer.Models;

namespace IrrigationSystemServer.Services;

public class ServerGrpcService : ServerGrpc.ServerGrpcBase
{
    private readonly ApplicationDbContext _context;
    public ServerGrpcService(ApplicationDbContext context)
    {
        _context = context;
    }
    public override async Task<Empty> SendSensorData(SensorData request, ServerCallContext context) // TODO: Think about a better name
    {
        Console.WriteLine("ClientId: " + request.ClientId);
        Console.WriteLine("SensorId: " + request.SensorId);
        Console.WriteLine("SensorReading" + request.SensorReading);

        var sensorData = new MoistureSensorData
        {
            MoistureSensorId = Guid.Parse(request.SensorId),
            SensorReading = request.SensorReading,
        };

        await _context.MoistureSensorData.AddAsync(sensorData);
        await _context.SaveChangesAsync();

        return new Empty();
    }
}