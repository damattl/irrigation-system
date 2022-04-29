using System.ComponentModel.DataAnnotations.Schema;

namespace IrrigationSystemServer.Models;

public class DeviceType
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid DeviceTypeId { get; set; }
    public string? Name { get; set; }
    public int SensorCount { get; set; }
    public int ValveCount { get; set; }
}