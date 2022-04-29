using System.ComponentModel.DataAnnotations.Schema;

namespace IrrigationSystemServer.Models;

public class Valve
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ValveId { get; set; }
    public Guid DeviceId { get; set; }
    public Device? Device { get; set; }
    public int PinId { get; set; }
}