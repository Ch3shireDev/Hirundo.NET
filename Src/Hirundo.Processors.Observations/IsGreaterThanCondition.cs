using Hirundo.Commons;

namespace Hirundo.Processors.Observations;

/// <summary>
///     Warunek sprawdzający, czy wartość obserwacji jest większa niż podana wartość.
/// </summary>
[TypeDescription("IsGreaterThan")]
public class IsGreaterThanCondition : IObservationCondition
{
    public IsGreaterThanCondition()
    {
    }

    public IsGreaterThanCondition(string valueName, object? value)
    {
        ValueName = valueName;
        Value = value;
    }

    public string ValueName { get; set; } = string.Empty;
    public object? Value { get; set; }

    public bool IsAccepted(Observation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);
        var observationValue = observation.GetValue(ValueName);
        if (DataTypeHelpers.IsGreaterThanNumeric(observationValue, Value)) return true;
        if (DataTypeHelpers.IsGreaterThanDate(observationValue, Value)) return true;
        return false;
    }
}
