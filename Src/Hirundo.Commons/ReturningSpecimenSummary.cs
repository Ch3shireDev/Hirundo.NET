namespace Hirundo.Commons;

/// <summary>
///     Zbiór wyników zawierający listę osobników powracających wraz z wartościami kluczowymi,
///     wartościami pomiarowymi oraz wartościami statystycznymi.
///     zbiór ustawień.
/// </summary>
public class ReturningSpecimenSummary
{
    /// <summary>
    ///     Osobnik powracający.
    /// </summary>
    public Specimen ReturningSpecimen { get; set; } = null!;

    /// <summary>
    ///     Dane populacji.
    /// </summary>
    public Specimen[] Population { get; set; } = [];

    /// <summary>
    ///     Dane statystyczne.
    /// </summary>
    public StatisticalData[] Statistics { get; set; } = [];
}