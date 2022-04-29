using IrrigationSystemServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace IrrigationSystemServer.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Device> Devices { get; set; }
    public DbSet<DeviceType> DeviceTypes { get; set; }
    public DbSet<Valve> Valves { get; set; }
    public DbSet<MoistureSensor> MoistureSensors { get; set; }
    public DbSet<MoistureSensorData> MoistureSensorData { get; set; }
    public DbSet<PlantProfile> PlantProfiles { get; set; }
    public DbSet<IrrigationProfile> IrrigationProfiles { get; set; }
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

    }
}