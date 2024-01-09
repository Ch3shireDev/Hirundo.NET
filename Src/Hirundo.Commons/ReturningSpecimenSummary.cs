namespace Hirundo.Commons;

/// <summary>
///     Zbiór wyników zawierający listę osobników powracających wraz z wartościami kluczowymi,
///     wartościami pomiarowymi oraz wartościami statystycznymi.
/// </summary>
public class ReturningSpecimenSummary(Specimen returningSpecimen, IList<Specimen> population, IList<StatisticalData> statistics)
{
    /// <summary>
    ///     Osobnik powracający.
    /// </summary>
    public Specimen ReturningSpecimen { get; } = returningSpecimen;

    /// <summary>
    ///     Dane populacji.
    /// </summary>
    public IList<Specimen> Population { get; } = population;

    /// <summary>
    ///     Dane statystyczne.
    /// </summary>
    public IList<StatisticalData> Statistics { get; } = statistics;

    public string[] GetHeaders()
    {
        var returningSpecimenHeaders = ReturningSpecimen.GetHeaders();
        var statisticsHeaders = Statistics.Select(s => s.Name).ToArray();
        return [..returningSpecimenHeaders, ..statisticsHeaders];
    }

    public object?[] GetValues()
    {
        var specimenData = ReturningSpecimen.GetValues();
        var statisticalData = Statistics.Select(s => s.Value);
        return [..specimenData, ..statisticalData];
    }
}