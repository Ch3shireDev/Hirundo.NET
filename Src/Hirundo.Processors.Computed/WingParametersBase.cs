namespace Hirundo.Processors.Computed;

public class WingParametersBase
{
    public virtual string[] WingParameters { get; set; } = ["D2", "D3", "D4", "D5", "D6", "D7", "D8"];
    public virtual string WingName { get; set; } = "WING";
    public virtual string ResultName { get; set; } = string.Empty;
}
