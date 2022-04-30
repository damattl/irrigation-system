using IrrigationSystemServer.Data;
using IrrigationSystemServer.Models;
using Microsoft.EntityFrameworkCore;

namespace IrrigationSystemServer.Services;

public class IrrigationProfileService
{
    private readonly ApplicationDbContext _context;

    public IrrigationProfileService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<IrrigationProfile>> GetAllIrrigationProfilesAsync()
    {
        return await _context.IrrigationProfiles
            .Include(p => p.PlantProfile)
            .ToListAsync();
    }
    
    public async Task<IrrigationProfile?> GetIrrigationProfileAsync(Guid id)
    {
        return await _context.IrrigationProfiles
            .Include(p => p.PlantProfile)
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
}