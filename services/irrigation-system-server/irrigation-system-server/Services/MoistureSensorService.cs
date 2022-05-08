using IrrigationSystemServer.Data;
using IrrigationSystemServer.Models;
using Microsoft.EntityFrameworkCore;

namespace IrrigationSystemServer.Services;

public class MoistureSensorService
{
    private readonly ApplicationDbContext _context;

    public MoistureSensorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MoistureSensor?> GetMoistureSensorAsync(Guid moistureSensorId)
    {
        return await _context.MoistureSensors.FindAsync(moistureSensorId);
    }
    
    public async Task<List<MoistureSensor>> GetAllMoistureSensorsAsync()
    {
        return await _context.MoistureSensors.ToListAsync();
    }

    public async Task<bool> InsertMoistureSensorAsync(MoistureSensor moistureSensor)
    {
        await _context.MoistureSensors.AddAsync(moistureSensor);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> DeleteMoistureSensorAsync(MoistureSensor moistureSensor)
    {
        _context.MoistureSensors.Remove(moistureSensor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<MoistureSensorData>> GetMoistureSensorDataAsync(Guid moistureSensorId, int count)
    {
        return await _context.MoistureSensorData
            .Where(d => d.MoistureSensorId == moistureSensorId)
            .OrderByDescending(d => d.TimeStamp)
            .Take(count)
            .Reverse()
            .ToListAsync();
    }
}