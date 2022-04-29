namespace IrrigationSystemServer.Models;

public class IrrigationProfile
{
    public Guid IrrigationProfileId { get; set; }
    public Guid DeviceId { get; set; }
    public Device? Device { get; set; }
    public Guid MoistureSensorId { get; set; }
    public MoistureSensor? MoistureSensor { get; set; }
    public Guid ValveId { get; set; }
    public Valve? Valve { get; set; }
    public Guid PlantProfileId { get; set; }
    public PlantProfile? PlantProfile { get; set; }
    
    // TODO: Add SensorData
}