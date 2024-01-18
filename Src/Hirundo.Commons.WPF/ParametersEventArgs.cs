namespace Hirundo.Commons.WPF;

public class ParametersEventArgs(object condition) : EventArgs
{
    public object Condition { get; } = condition;
}