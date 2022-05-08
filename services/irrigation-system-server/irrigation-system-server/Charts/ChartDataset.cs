namespace IrrigationSystemServer.Charts;

public class ChartDataset<T>
{
    public string? Label { get; set; }
    public IEnumerable<T>? Data { get; set; }
    public bool? Fill { get; set; }
    public string? BorderColor { get; set; }
    public double? Tension { get; set; }
    public bool? ShowLine { get; set; }
    public string YAxisId = "y";
}