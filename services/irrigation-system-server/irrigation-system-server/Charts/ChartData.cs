namespace IrrigationSystemServer.Charts;

public class ChartData<T>
{
    public IEnumerable<object> Labels { get; set; }
    public IEnumerable<ChartDataset<T>> Datasets { get; set; }
}