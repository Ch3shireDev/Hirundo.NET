namespace Hirundo.Commons.WPF;

public class ConditionEventArgs(object condition) : EventArgs
{
    public object Condition { get; } = condition;
}