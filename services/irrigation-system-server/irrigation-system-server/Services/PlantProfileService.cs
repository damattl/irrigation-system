using IrrigationSystemServer.Data;
using IrrigationSystemServer.Models;
using Microsoft.EntityFrameworkCore;

namespace IrrigationSystemServer.Services;

public class PlantProfileService
{
    private readonly ApplicationDbContext _context;

    public PlantProfileService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PlantProfile>> GetAllPlantProfilesAsync()
    {
        return await _context.PlantProfiles
            .ToListAsync();
    }

    public async Task<bool> InsertPlantProfileAsync(PlantProfile plantProfile)
    {
        await _context.PlantProfiles
            .AddAsync(plantProfile);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> DeletePlantProfileAsync(PlantProfile plantProfile)
    {
        _context.PlantProfiles.Remove(plantProfile);
        await _context.SaveChangesAsync();
        return true;
    }
}