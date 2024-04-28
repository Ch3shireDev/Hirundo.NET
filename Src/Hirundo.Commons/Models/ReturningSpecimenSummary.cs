namespace Hirundo.Commons.Models;

/// <summary>
///     Zbiór wyników zawierający listę osobników powracających wraz z wartościami kluczowymi,
///     wartościami pomiarowymi oraz wartościami statystycznymi.
/// </summary>
public class ReturningSpecimenSummary(IReadOnlyList<string> headers, IReadOnlyList<object?> values)
{
    public string Ring { get; set; } = "123";
    public DateTime DateFirstSeen { get; set; } = new(2020, 06, 01);
    public DateTime DateLastSeen { get; set; } = new(2021, 06, 01);

    public IReadOnlyList<string> Headers { get; } = headers;
    public IReadOnlyList<object?> Values { get; } = values;

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
}