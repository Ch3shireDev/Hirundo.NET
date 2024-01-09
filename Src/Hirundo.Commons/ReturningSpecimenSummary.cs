namespace Hirundo.Commons;

/// <summary>
///     Zbiór wyników zawierający listę osobników powracających wraz z wartościami kluczowymi,
///     wartościami pomiarowymi oraz wartościami statystycznymi.
/// </summary>
public class ReturningSpecimenSummary(Specimen returningSpecimen, Specimen[] population, StatisticalData[] statistics)
{
    /// <summary>
    ///     Osobnik powracający.
    /// </summary>
    public Specimen ReturningSpecimen { get; set; } = returningSpecimen;

    /// <summary>
    ///     Dane populacji.
    /// </summary>
    public Specimen[] Population { get; set; } = population;

    /// <summary>
    ///     Dane statystyczne.
    /// </summary>
    public StatisticalData[] Statistics { get; set; } = statistics;

    public string[] GetHeaders()
    {
        var returningSpecimenHeaders = ReturningSpecimen.GetHeaders();
        var statisticsHeaders = Statistics.Select(s => s.Name).ToArray();
        return returningSpecimenHeaders.Concat(statisticsHeaders).ToArray();
    }

    public object?[] GetValues()
    {
        var specimenData = ReturningSpecimen.GetValues();
        var statisticalData = Statistics.Select(s => s.Value);
        return [..specimenData, ..statisticalData];
    }
}