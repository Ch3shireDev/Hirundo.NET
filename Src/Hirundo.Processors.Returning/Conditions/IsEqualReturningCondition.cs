using Hirundo.Commons;

namespace Hirundo.Processors.Returning.Conditions;

public class IsEqualReturningCondition : IReturningSpecimenCondition
{
    public IsEqualReturningCondition()
    {
    }

    public IsEqualReturningCondition(string valueName, object? value)
    {
        ValueName = valueName;
        Value = value;
    }

    public string ValueName { get; set; } = string.Empty;
    public object? Value { get; set; }

    public bool IsReturning(Specimen specimen)
    {
        return specimen.Observations.Any(o => o.GetValue(ValueName)?.Equals(Value) ?? Value == null);
    }
}