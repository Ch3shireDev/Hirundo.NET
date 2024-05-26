using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Observations;

/// <summary>
///     Warunek sprawdzający, czy wartość obserwacji jest większa niż podana wartość.
/// </summary>
[TypeDescription(
    "IsLowerOrEqual",
    "Czy wartość jest mniejsza lub równa?",
    "Warunek sprawdzający, czy pole danych jest mniejsze lub równe podanej wartości.\nW przypadku dat, wartości należy podawać w formacie YYYY-MM-DD."
)]
public class IsLowerOrEqualCondition : IObservationCondition, ICompareValueCondition, ISelfExplainer
{
    public IsLowerOrEqualCondition()
    {
    }

    public IsLowerOrEqualCondition(string valueName, object? value)
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
        return ComparisonHelpers.IsLowerOrEqual(observationValue, Value);
    }

    public string Explain()
    {
        return $"Wartość {ValueName} musi być mniejsza lub równa {Value}";
    }
}