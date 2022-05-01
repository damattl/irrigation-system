namespace IrrigationSystemServer.Charts;

public class ChartOptions
{
    public bool MainaintainAspectRatio { get; set; } = true;
    public object Scales { get; set; }
    public ChartOptionsElements Elements { get; set; }
}