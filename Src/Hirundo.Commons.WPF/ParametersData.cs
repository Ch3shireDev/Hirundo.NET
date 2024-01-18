namespace Hirundo.Commons.WPF;

public class ParametersData
{
    public ParametersData()
    {
    }

    public ParametersData(Type type, string title = "", string description = "")
    {
        Type = type;
        Title = title;
        Description = description;
    }

    public Type Type { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}