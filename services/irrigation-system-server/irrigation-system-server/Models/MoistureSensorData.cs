using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace IrrigationSystemServer.Models;

public class MoistureSensorData
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid MoistureSensorDataId { get; set; }
    public Guid MoistureSensorId { get; set; }
    public MoistureSensor? MoistureSensor { get; set; }
    public float SensorReading { get; set; }
    public long TimeStamp { get; set; } = DateTime.Now.ToBinary();
}