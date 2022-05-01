namespace IrrigationSystemServer.Charts;

public class LineChartConfig : ChartConfig<float>
{
    public LineChartConfig(ChartData<float> data, ChartOptions? options = null)
    {
        Type = "line";
        if (data.Datasets.Count() != 0)
        {
            data.Datasets.ToArray()[0].ShowLine = true;
        }
        Data = data;
        Options = options;
    }
}