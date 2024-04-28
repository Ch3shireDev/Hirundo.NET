using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Observations;

/// <summary>
///     Warunek sprawdzający, czy wartość obserwacji jest mniejsza niż podana wartość.
/// </summary>
[TypeDescription(
    "IsLowerThan",
    "Czy wartość jest mniejsza niż?",
    "Warunek sprawdzający, czy pole danych jest mniejsze niż podana wartość.\nDaty należy podawać w formacie YYYY-MM-DD."
)]
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