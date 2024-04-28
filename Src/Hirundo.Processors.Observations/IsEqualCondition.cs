using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Observations;

/// <summary>
///     Warunek sprawdzający, czy wartość obserwacji jest równa podanej wartości.
/// </summary>
[TypeDescription(
    "IsEqual",
    "Czy wartość jest równa?",
    "Warunek porównujący pole danych z podaną wartością."
)]
public class IsEqualCondition : ICompareValueCondition, IObservationCondition
{
    /// <summary>
    ///     Domyślny konstruktor. Ustawia wartości domyślne jako pusty string.
    /// </summary>
    public IsEqualCondition()
    {
        ValueName = string.Empty;
        Value = string.Empty;
    }

    /// <summary>
    ///     Warunek sprawdzający, czy wartość obserwacji jest równa podanej wartości.
    /// </summary>
    /// <param name="valueName">Nazwa sprawdzanej wartości obserwacji.</param>
    /// <param name="value">Wartość, do której jest przyrównywana wartość obserwacji.</param>
    public IsEqualCondition(string valueName, object? value)
    {
        ValueName = valueName;
        Value = value;
    }

    /// <summary>
    ///     Nazwa sprawdzanej wartości obserwacji.
    /// </summary>
    public string ValueName { get; set; }

    /// <summary>
    ///     Wartość, do której jest przyrównywana wartość obserwacji.
    /// </summary>
    public object? Value { get; set; }

    /// <summary>
    ///     Czy wartość obserwacji jest równa podanej wartości?
    ///     Porównywany jest zarówno typ, jak i wartość.
    /// </summary>
    /// <param name="observation"></param>
    /// <returns></returns>
    public bool IsAccepted(Observation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);
        var observationValue = observation.GetValue(ValueName);
        return ComparisonHelpers.IsEqual(observationValue, Value);
    }
}