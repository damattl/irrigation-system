using IrrigationSystemServer.Data;
using IrrigationSystemServer.Models;
using Microsoft.EntityFrameworkCore;

namespace IrrigationSystemServer.Services;

public class IrrigationProfileService
{
    private readonly ApplicationDbContext _context;
    private readonly BrokerGrpc.BrokerGrpcClient _grpc;

    public IrrigationProfileService(ApplicationDbContext context, BrokerGrpc.BrokerGrpcClient grpc)
    {
        _context = context;
        _grpc = grpc;
    }

    public async Task<List<IrrigationProfile>> GetAllIrrigationProfilesAsync()
    {
        return await _context.IrrigationProfiles
            .Include(p => p.PlantProfile)
            .Include(p => p.MoistureSensor)
            .Include(p => p.Valve)
            .ToListAsync();
    }
    
    public async Task<IrrigationProfile?> GetIrrigationProfileAsync(Guid id)
    {
        return await _context.IrrigationProfiles
            .Include(p => p.PlantProfile)
            .Include(p => p.Valve)
            .Include(p => p.MoistureSensor)
            .FirstAsync(p => p.IrrigationProfileId == id);
    }

    public async Task<bool> InsertIrrigationProfileAsync(IrrigationProfile irrigationProfile)
    {
        await _context.IrrigationProfiles
            .AddAsync(irrigationProfile);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> DeleteIrrigationProfileAsync(IrrigationProfile irrigationProfile)
    {
        _context.IrrigationProfiles.Remove(irrigationProfile);
        await _context.SaveChangesAsync();
        return true;
    }

    public void OpenValve(OpenValveRequest request)
    {
        _grpc.OpenValve(request);
    }

    public void ReadSensor(ReadSensorRequest request)
    {
        _grpc.ReadSensor(request);
    }
}