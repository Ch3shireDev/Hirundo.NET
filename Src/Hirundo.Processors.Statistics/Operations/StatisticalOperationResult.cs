namespace Hirundo.Processors.Statistics.Operations;

/// <summary>
///     Wynik operacji statystycznej.
/// </summary>
public class StatisticalOperationResult
{
    public StatisticalOperationResult(string name, object value)
    {
        Names = [name];
        Values = [value];
    }

    public StatisticalOperationResult(string name, object value, IList<object> populationIds)
    {
        Names = [name];
        Values = [value];
        PopulationIds = populationIds;
    }

    public StatisticalOperationResult(string name, object value, IList<object> populationIds, IList<object> emptyValuesIds)
    {
        Names = [name];
        Values = [value];
        PopulationIds = populationIds;
        EmptyValuesIds = emptyValuesIds;
    }

    public StatisticalOperationResult(IList<string> names, IList<object> values)
    {
        Names = names;
        Values = values;
    }

    public StatisticalOperationResult(IList<string> names, IList<object> values, IList<object> populationIds)
    {
        Names = names;
        Values = values;
        PopulationIds = populationIds;
    }

    public StatisticalOperationResult(IList<string> names, IList<object> values, IList<object> populationIds, IList<object> emptyValuesIds)
    {
        Names = names;
        Values = values;
        PopulationIds = populationIds;
        EmptyValuesIds = emptyValuesIds;
    }

    /// <summary>
    ///     Nazwa wartości statystycznej.
    /// </summary>
    public IList<string> Names { get; init; }

    /// <summary>
    ///     Wartość statystyczna.
    /// </summary>
    public IList<object> Values { get; init; }

    /// <summary>
    ///     Identyfikatory populacji, dla których została wyznaczona wartość statystyczna.
    /// </summary>
    public IList<object> PopulationIds { get; init; } = [];

    /// <summary>
    ///     Identyfikatory populacji, dla których wartość statystyczna jest pusta.
    /// </summary>
    public IList<object> EmptyValuesIds { get; init; } = [];
}