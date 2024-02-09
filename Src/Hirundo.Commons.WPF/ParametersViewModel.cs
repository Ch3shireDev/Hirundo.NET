namespace Hirundo.Commons.WPF;

public class ParametersViewModel : ViewModelBase
{
    public string Type { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}