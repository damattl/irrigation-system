namespace IrrigationSystemServer.Charts;

public class ChartOptions
{
    public bool MaintainAspectRatio { get; set; } = false;
    public bool Responsive { get; set; } = true;
    public object Scales { get; set; }
    public ChartOptionsElements Elements { get; set; }
}