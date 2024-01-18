namespace Hirundo.Commons.WPF;

public class ParametersEventArgs(object parameters) : EventArgs
{
    public object Parameters { get; } = parameters;
}