namespace Hirundo.Commons;

/// <summary>
///     Klasa reprezentująca zbiór wszystkich osobników opisywanych przez obserwacje spełniające zadany zestaw
///     warunków. Populację dla danego osobnika powracającego ustala się na podstawie zestawu warunków określanych przez
///     użytkownika - np. populacja musi być tego samego gatunku, płci, musi być zaobserwowana w tym samym miejscu, etc.
/// </summary>
public class PopulationData
{
    /// <summary>
    ///     Osobniki spełniające warunki populacji.
    /// </summary>
    public List<Specimen> Specimens { get; set; } = [];
}