namespace Hirundo.Processors.Observations;

public interface ICompareValueCondition
{
    public string ValueName { get; set; }

    public object? Value { get; set; }
}
