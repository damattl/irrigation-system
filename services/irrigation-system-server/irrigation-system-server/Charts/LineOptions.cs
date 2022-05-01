namespace IrrigationSystemServer.Charts;

public class LineOptions
{
    public double? Tension = 0;
    public string? BackgroundColor = "rgba(0, 0, 0, 0.1)";
    public double? BorderWidth = 3;
    public string? BorderColor = "rgba(0, 0, 0, 0.1)";
    public string? BorderCapStyle = "butt";
    public double[]? BorderDash = Array.Empty<double>();
    public double? BorderDashOffset = 0.0;

    /// <summary>
    /// Accepted values 'round'|'bevel'|'miter'
    /// </summary>
    public string? BorderJoinStyle = "miter";

    public bool? CapBezierPoints = true;
    public string? CubicInterpolationMode = "default";
    public bool? Fill = false;
    public bool? Stepped = false;
}