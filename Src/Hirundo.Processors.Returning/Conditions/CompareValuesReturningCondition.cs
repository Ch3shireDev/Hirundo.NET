using Hirundo.Commons.Models;

namespace Hirundo.Processors.Returning.Conditions;

public abstract class CompareValuesReturningCondition : IReturningSpecimenCondition
{
    public CompareValuesReturningCondition()
    {
    }

    public CompareValuesReturningCondition(string valueName, object? value)
    {
        ValueName = valueName;
        Value = value;
    }

    public string ValueName { get; set; } = string.Empty;
    public object? Value { get; set; }
    public bool IsReturning(Specimen specimen)
    {
        return specimen.Observations.Any(o => Compare(o.GetValue(ValueName), Value));
    }

    public abstract bool Compare(object? observationValue, object? value);
}
