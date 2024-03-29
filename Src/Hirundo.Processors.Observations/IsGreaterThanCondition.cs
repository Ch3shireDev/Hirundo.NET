﻿using Hirundo.Commons;

namespace Hirundo.Processors.Observations;

/// <summary>
///     Warunek sprawdzający, czy wartość obserwacji jest większa niż podana wartość.
/// </summary>
[TypeDescription("IsGreaterThan")]
public class IsGreaterThanCondition : IObservationCondition, ICompareValueCondition
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
        return ComparisonHelpers.IsGreater(observationValue, Value);
    }
}
