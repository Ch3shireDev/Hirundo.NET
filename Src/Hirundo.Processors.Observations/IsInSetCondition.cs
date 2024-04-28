using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Observations;

/// <summary>
///     Warunek sprawdzający, czy wartość obserwacji z zadanego pola znajduje się w zbiorze wartości.
/// </summary>
[TypeDescription("IsInSet",
    "Czy dane są w zbiorze?",
    "Warunek sprawdzający, czy pole danych znajduje się w zbiorze wartości.")]
public class IsInSetCondition : IObservationCondition, ISelfExplainer
{
    /// <summary>
    ///     Konstruktor domyślny. Ustawia wartości domyślne jako pusty string i pustą listę.
    /// </summary>
    public IsInSetCondition()
    {
        ValueName = string.Empty;
        Values = new List<object>();
    }

    /// <summary>
    ///     Konstruktor warunku sprawdzającego, czy wartość obserwacji z zadanego pola znajduje się w zbiorze wartości.
    /// </summary>
    /// <param name="valueName">Nazwa wartości.</param>
    /// <param name="values">Dostępne wartości obserwacji.</param>
    public IsInSetCondition(string valueName, params object[] values)
    {
        ValueName = valueName;
        Values = values;
    }

    /// <summary>
    ///     Nazwa wartości.
    /// </summary>
    public string ValueName { get; set; }

    /// <summary>
    ///     Dostępne wartości obserwacji.
    /// </summary>
    public IList<object> Values { get; set; }

    /// <summary>
    ///     Czy wartość obserwacji z zadanego pola znajduje się w zbiorze wartości?
    ///     Porównywany jest zarówno typ, jak i wartość.
    /// </summary>
    /// <param name="observation"></param>
    /// <returns></returns>
    public bool IsAccepted(Observation observation)
    {
        var value = observation.GetValue(ValueName);
        return value != null && Values.Contains(value);
    }

    public string Explain()
    {
        return $"Wartość {ValueName} musi być jedną z wartości: {string.Join(", ", Values)}";
    }
}