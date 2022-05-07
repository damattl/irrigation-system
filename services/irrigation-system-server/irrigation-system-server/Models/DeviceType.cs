using System.ComponentModel.DataAnnotations.Schema;

namespace IrrigationSystemServer.Models;

public class DeviceType
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid DeviceTypeId { get; set; }
    public string? Name { get; set; }
    public int SensorCount => SensorPinMap.Count;
    public int ValveCount => ValvePinMap.Count;

    public Dictionary<int, string> SensorPinMap { get; set; } = new Dictionary<int, string>();
    public Dictionary<int, string> ValvePinMap { get; set; } = new Dictionary<int, string>();

    // TODO: Add PIN MAP
}