using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Observations;

/// <summary>
///     Warunek sprawdzający, czy wartość obserwacji jest większa niż podana wartość.
/// </summary>
[TypeDescription("IsGreaterOrEqual",
    "Czy wartość jest większa lub równa?",
    "Warunek sprawdzający, czy pole danych jest większe lub równe podanej wartości.\nW przypadku dat, wartości należy podawać w formacie YYYY-MM-DD.")]
public class IsGreaterOrEqualCondition : IObservationCondition, ICompareValueCondition, ISelfExplainer
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

    public string Explain()
    {
        return $"Wartość {ValueName} musi być większa lub równa {Value}";
    }
}