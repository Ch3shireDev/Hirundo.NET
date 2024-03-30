using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Observations;

/// <summary>
///     Warunek sprawdzający, czy wartość obserwacji jest mniejsza niż podana wartość.
/// </summary>
[TypeDescription("IsLowerThan")]
public class IsLowerThanCondition : IObservationCondition, ICompareValueCondition
{
    public IsLowerThanCondition()
    {
    }

    public IsLowerThanCondition(string valueName, object? value)
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
        return ComparisonHelpers.IsLower(observationValue, Value);
    }
}
