using Hirundo.Commons;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription("IsInSet")]
public class IsInSetReturningCondition : IReturningSpecimenCondition
{
    public IsInSetReturningCondition()
    {
    }

    public IsInSetReturningCondition(string valueName, IList<object> values)
    {
        ValueName = valueName;
        Values = values;
    }

    public string ValueName { get; set; } = string.Empty;
    public IList<object> Values { get; set; } = [];

    public bool IsReturning(Specimen specimen)
    {
        return specimen.Observations.Any(IsInSet);
    }

    private bool IsInSet(Observation observation)
    {
        var value = observation.GetValue(ValueName);
        return Values.Any(v => v.Equals(value));
    }
}