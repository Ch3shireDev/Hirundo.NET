namespace Hirundo.Commons.Models;

public class ReturningSpecimensResults
{
    public IList<ReturningSpecimenSummary> Results { get; init; } = [];
    public string Explanation { get; set; } = string.Empty;
}

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
        var returningSpecimenHeaders = GetValueHeaders();
        var statisticsHeaders = GetStatisticsHeaders();
        return [.. returningSpecimenHeaders, .. statisticsHeaders];
    }

    public object?[] GetValues()
    {
        var specimenData = ReturningSpecimen.GetValues();
        var statisticalData = Statistics.Select(s => s.Value);
        return [.. specimenData, .. statisticalData];
    }

    public object?[] GetValues(string[] headers)
    {
        ArgumentNullException.ThrowIfNull(headers);

        var output = new object?[headers.Length];

        for (var i = 0; i < headers.Length; i++)
        {
            var header = headers[i];

            if (ReturningSpecimen.GetHeaders().Contains(header))
            {
                output[i] = ReturningSpecimen.GetValue(header);
            }
            else
            {
                output[i] = Statistics.FirstOrDefault(s => s.Name == header)?.Value;
            }
        }

        return output;
    }

    public string[] GetValueHeaders()
    {
        return ReturningSpecimen.GetHeaders();
    }

    public string[] GetStatisticsHeaders()
    {
        return Statistics.Select(s => s.Name).ToArray();
    }
}