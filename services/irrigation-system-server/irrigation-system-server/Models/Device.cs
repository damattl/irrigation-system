using System.ComponentModel.DataAnnotations.Schema;

namespace IrrigationSystemServer.Models;

public class Device
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid DeviceId { get; set; }
    public Guid TypeId { get; set; }
    public DeviceType? Type { get; set; }
    public List<MoistureSensor>? MoistureSensors { get; set; } = new();
    public List<Valve>? Valves { get; set; } = new();
}