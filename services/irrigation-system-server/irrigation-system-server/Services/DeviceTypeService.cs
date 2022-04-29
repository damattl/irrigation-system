using IrrigationSystemServer.Data;
using IrrigationSystemServer.Models;
using Microsoft.EntityFrameworkCore;

namespace IrrigationSystemServer.Services;

public class DeviceTypeService
{
    private readonly ApplicationDbContext _context;

    public DeviceTypeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DeviceType>> GetAllDeviceTypesAsync()
    {
        return await _context.DeviceTypes.ToListAsync();
    }

    public async Task<bool> InsertDeviceTypeAsync(DeviceType deviceType)
    {
        await _context.DeviceTypes.AddAsync(deviceType);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> DeleteDeviceTypeAsync(DeviceType deviceType)
    {
        _context.DeviceTypes.Remove(deviceType);
        await _context.SaveChangesAsync();
        return true;
    }
}