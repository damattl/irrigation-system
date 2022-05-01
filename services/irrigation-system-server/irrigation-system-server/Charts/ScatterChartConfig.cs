namespace IrrigationSystemServer.Charts;

public class ScatterChartConfig : ChartConfig<object>
{
    public ScatterChartConfig(ChartData<object> data, ChartOptions? options = null)
    {
        Type = "scatter";
        Data = data;
        Options = options;
    }
}