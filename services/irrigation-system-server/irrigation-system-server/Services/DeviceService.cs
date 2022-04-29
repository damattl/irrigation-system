using IrrigationSystemServer.Data;
using IrrigationSystemServer.Models;
using Microsoft.EntityFrameworkCore;

namespace IrrigationSystemServer.Services;

public class DeviceService
{
    private readonly ApplicationDbContext _context;

    public DeviceService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Device>> GetAllDevicesAsync()
    {
        return await _context.Devices
            .Include(d => d.Type)
            .ToListAsync();
    }
    
    public async Task<Device?> GetDeviceByIdAsync(Guid deviceId)
    {
        return await _context.Devices
            .Include(d => d.Valves)
            .Include(d => d.MoistureSensors)
            .FirstOrDefaultAsync(d => d.DeviceId == deviceId);
    }

    public async Task<bool> InsertDeviceAsync(Device device)
    {
        var type = await _context.DeviceTypes.FindAsync(device.TypeId);
        for (int i = 0; i < type?.SensorCount; i++)
        {
            device.MoistureSensors?.Add(new MoistureSensor{PinId = i + 1});
        }
        for (int i = 0; i < type?.ValveCount; i++)
        {
            device.Valves?.Add(new Valve{PinId = i + 1});
        }
        
        Console.WriteLine(device.Valves);
        await _context.Devices
            .AddAsync(device);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> DeleteDeviceAsync(Device device)
    {
        _context.Devices.Remove(device);
        await _context.SaveChangesAsync();
        return true;
    }
}