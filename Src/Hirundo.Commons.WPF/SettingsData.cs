namespace Hirundo.Commons.WPF;

public class SettingsData
{
    public SettingsData()
    {
    }

    public SettingsData(Type type, string title = "", string description = "")
    {
        Type = type;
        Title = title;
        Description = description;
    }

    public Type Type { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}