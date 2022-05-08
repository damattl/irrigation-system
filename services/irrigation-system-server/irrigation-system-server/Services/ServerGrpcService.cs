using Grpc.Core;
using IrrigationSystemServer.Data;
using IrrigationSystemServer.Models;
using Microsoft.EntityFrameworkCore;

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
        Console.WriteLine("SensorId: " + request.SensorPin);
        Console.WriteLine("SensorReading: " + request.SensorReading);

        var device = await _context.Devices
            .Include(d => d.MoistureSensors)
            .FirstOrDefaultAsync(d => d.DeviceId == Guid.Parse(request.ClientId));

        var sensor = device?.MoistureSensors?.FirstOrDefault(s => s.PinId == request.SensorPin);
        if (sensor == null)
        {
            return new Empty();
        }

        var sensorData = new MoistureSensorData
        {
            MoistureSensorId = sensor.MoistureSensorId,
            SensorReading = request.SensorReading,
        };

        await _context.MoistureSensorData.AddAsync(sensorData);
        await _context.SaveChangesAsync();

        return new Empty();
    }
}