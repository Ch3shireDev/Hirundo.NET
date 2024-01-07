namespace Hirundo.Commons;

/// <summary>
///     Wartość statystyczna.
/// </summary>
public class StatisticalDataValue
{
    public StatisticalDataValue()
    {
    }

    public StatisticalDataValue(string name, object value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; set; } = null!;
    public object Value { get; set; } = null!;
}