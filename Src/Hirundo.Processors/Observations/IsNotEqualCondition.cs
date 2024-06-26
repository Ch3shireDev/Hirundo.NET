﻿using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Observations;

[TypeDescription("IsNotEqual",
    "Czy wartość nie jest równa?",
    "Warunek porównujący pole danych z podaną wartością.")]
public class IsNotEqualCondition : IObservationCondition, ICompareValueCondition, ISelfExplainer
{
    public IsNotEqualCondition()
    {
    }

    public IsNotEqualCondition(string valueName, object? value)
    {
        ValueName = valueName;
        Value = value;
    }

    public string ValueName { get; set; } = null!;
    public object? Value { get; set; }

    public bool IsAccepted(Observation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);
        var observationValue = observation.GetValue(ValueName);
        return !ComparisonHelpers.IsEqual(Value, observationValue);
    }

    public string Explain()
    {
        return $"Wartość {ValueName} nie może być równa '{Value}'";
    }
}