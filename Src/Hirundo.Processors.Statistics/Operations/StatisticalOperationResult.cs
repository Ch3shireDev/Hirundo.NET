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

    public StatisticalOperationResult(string name, object value, IList<object> populationIds, IList<object> emptyValueIds)
    {
        Names = [name];
        Values = [value];
        PopulationIds = populationIds;
        EmptyValueIds = emptyValueIds;
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

    public StatisticalOperationResult(IList<string> names, IList<object> values, IList<object> populationIds, IList<object> emptyValueIds)
    {
        Names = names;
        Values = values;
        PopulationIds = populationIds;
        EmptyValueIds = emptyValueIds;
    }

    public StatisticalOperationResult(IList<string> names, IList<object> values, IList<object> populationIds, IList<object> emptyValueIds, IList<object> outlierIds)
    {
        Names = names;
        Values = values;
        PopulationIds = populationIds;
        EmptyValueIds = emptyValueIds;
        OutlierIds = outlierIds;
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
    ///     Identyfikatory populacji, dla których została wyznaczona niepusta wartość statystyczna.
    /// </summary>
    public IList<object> PopulationIds { get; init; } = [];

    /// <summary>
    ///     Identyfikatory populacji, dla których wartość statystyczna jest pusta.
    /// </summary>
    public IList<object> EmptyValueIds { get; init; } = [];

    /// <summary>
    ///     Identyfikatory populacji, dla których wartość statystyczna jest odstająca.
    /// </summary>
    public IList<object> OutlierIds { get; init; } = [];
}