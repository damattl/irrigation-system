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
    public string? Name { get; set; }
    public int WaterConsumption { get; set;  }
    public int Threshold { get; set; }
    
    // TODO: Add SensorData
}