namespace Hirundo.Commons.Models;

/// <summary>
///     Zbiór wyników zawierający listę osobników powracających wraz z wartościami kluczowymi,
///     wartościami pomiarowymi oraz wartościami statystycznymi.
/// </summary>
public class ReturningSpecimenSummary
{
    public ReturningSpecimenSummary(string[] headers, object?[] values)
    {
        Headers = headers;
        Values = values;
    }

    public IReadOnlyList<string> Headers { get; }
    public IReadOnlyList<object?> Values { get; }

    public object?[] SelectValues(IList<string> headers)
    {
        ArgumentNullException.ThrowIfNull(headers);

        var allValues = Values;
        var allHeaders = Headers.ToArray();

        var output = new object?[headers.Count];
        foreach (var header in allHeaders)
        {
            if (!headers.Contains(header)) continue;
            output[headers.IndexOf(header)] = allValues[Array.IndexOf(allHeaders, header)];
        }

        return output;
    }

    public static string[] GetHeadersInternal(Specimen returningSpecimen, IList<StatisticalData> statistics)
    {
        ArgumentNullException.ThrowIfNull(returningSpecimen);
        ArgumentNullException.ThrowIfNull(statistics);

        var returningSpecimenHeaders = returningSpecimen.GetHeaders();
        var statisticsHeaders = statistics.Select(s => s.Name).ToArray();
        return [.. returningSpecimenHeaders, .. statisticsHeaders];
    }

    public static object?[] GetValuesInternal(Specimen returningSpecimen, IList<StatisticalData> statistics)
    {
        ArgumentNullException.ThrowIfNull(returningSpecimen);
        ArgumentNullException.ThrowIfNull(statistics);

        var specimenData = returningSpecimen.GetValues();
        var statisticalData = statistics.Select(s => s.Value);
        return [.. specimenData, .. statisticalData];
    }
}