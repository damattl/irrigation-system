using System.ComponentModel.DataAnnotations.Schema;

namespace IrrigationSystemServer.Models;

public class MoistureSensor
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid MoistureSensorId { get; set; }
    public Guid DeviceId { get; set; }
    public Device? Device { get; set; }
    public int PinId { get; set; }
    public List<MoistureSensorData> MoistureSensorData { get; set; } = new();
}