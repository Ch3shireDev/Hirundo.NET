﻿using Hirundo.Commons;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription("IsNotEqual")]
public class IsNotEqualReturningCondition : CompareValuesReturningCondition
{
    public IsNotEqualReturningCondition(string valueName, object? value) : base(valueName, value) { }
    public IsNotEqualReturningCondition() : base() { }
    public override bool Compare(object? observationValue, object? value)
    {
        return observationValue != null && !ComparisonHelpers.IsEqual(observationValue, value);
    }
}
