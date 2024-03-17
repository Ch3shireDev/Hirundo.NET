using Hirundo.Commons;

namespace Hirundo.Processors.Observations;

/// <summary>
///     Warunek sprawdzający, czy wartość obserwacji jest większa niż podana wartość.
/// </summary>
[TypeDescription("IsGreaterOrEqual")]
public class IsGreaterOrEqualCondition : IObservationCondition, ICompareValueCondition
{
    public IsGreaterOrEqualCondition()
    {
    }

    public IsGreaterOrEqualCondition(string valueName, object? value)
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
        return ComparisonHelpers.IsGreaterOrEqual(observationValue, Value);
    }
}
