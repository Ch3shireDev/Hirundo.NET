using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.IsInSet;

public class IsInSetReturningModel(IsInSetReturningCondition condition, IDataLabelRepository repository)
{
    public IDataLabelRepository Repository { get; } = repository;
    public IsInSetReturningCondition Condition { get; init; } = condition;

    public string ValueName
    {
        get => Condition.ValueName;
        set => Condition.ValueName = value;
    }

    public DataType ValueType { get; set; }
    public IList<object> Values => Condition.Values;

    public void SetValues(IEnumerable<string> values)
    {
        Values.Clear();

        foreach (var value in values)
        {
            var castValue = DataTypeHelpers.GetValueSetValueFromString(value, ValueType);
            Values.Add(castValue);
        }
    }

    public void SetValue(string value, int index)
    {
        Values[index] = DataTypeHelpers.GetValueSetValueFromString(value, ValueType);
    }
}