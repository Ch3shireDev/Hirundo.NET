namespace Hirundo.Commons.WPF;

public class ParametersData
{
    public ParametersData()
    {
    }

    public ParametersData(Type conditionType, string title = "", string description = "")
    {
        ConditionType = conditionType;
        Title = title;
        Description = description;
    }

    public Type ConditionType { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}