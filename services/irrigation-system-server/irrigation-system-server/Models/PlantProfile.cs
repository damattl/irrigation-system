using System.ComponentModel.DataAnnotations.Schema;

namespace IrrigationSystemServer.Models;

public class PlantProfile
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid PlantProfileId { get; set; }
    public int WaterConsumption { get; set; }
    public string? Name { get; set; }
}