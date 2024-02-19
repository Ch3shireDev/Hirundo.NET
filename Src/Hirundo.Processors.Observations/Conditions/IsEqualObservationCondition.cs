using Hirundo.Commons;

namespace Hirundo.Processors.Observations.Conditions;

/// <summary>
///     Warunek sprawdzający, czy wartość obserwacji jest równa podanej wartości.
/// </summary>
[TypeDescription("IsEqual")]
public class IsEqualObservationCondition : IObservationCondition
{
    /// <summary>
    ///     Domyślny konstruktor. Ustawia wartości domyślne jako pusty string.
    /// </summary>
    public IsEqualObservationCondition()
    {
        ValueName = string.Empty;
        Value = string.Empty;
    }

    /// <summary>
    ///     Warunek sprawdzający, czy wartość obserwacji jest równa podanej wartości.
    /// </summary>
    /// <param name="valueName">Nazwa sprawdzanej wartości obserwacji.</param>
    /// <param name="value">Wartość, do której jest przyrównywana wartość obserwacji.</param>
    public IsEqualObservationCondition(string valueName, object value)
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
    public object Value { get; set; }

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
        if (observationValue == null)
        {
            return Value == null;
        }

        var observationType = observationValue.GetType();

        if (Value.GetType() == observationType) return Equals(Value, observationValue);

        var typeValue = DataTypeHelpers.ConvertValue(Value, observationType);
        return Equals(typeValue, observationValue);
    }
}