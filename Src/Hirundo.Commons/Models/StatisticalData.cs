namespace Hirundo.Commons;

/// <summary>
///     Wartość statystyczna, wyznaczana poprzez operator na podstawie populacji.
/// </summary>
public class StatisticalData
{
    public StatisticalData()
    {
    }

    public StatisticalData(string name, object? value)
    {
        Name = name;
        Value = value;
    }

    /// <summary>
    ///     Nazwa wartości statystycznej.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Wartość statystyczna.
    /// </summary>
    public object? Value { get; set; } = null!;
}