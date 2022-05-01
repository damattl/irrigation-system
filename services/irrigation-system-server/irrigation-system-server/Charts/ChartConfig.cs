namespace IrrigationSystemServer.Charts;

public class ChartConfig<T>
{
    public string? Type { get; set; }
    public ChartData<T>? Data { get; set; }
    public ChartOptions? Options { get; set; }
}