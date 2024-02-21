using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.IsInSet;

public class IsInSetReturningModel(IsInSetReturningCondition condition, IDataLabelRepository repository) : ParametersModel(condition, repository)
{
    public string ValueName
    {
        get => condition.ValueName;
        set => condition.ValueName = value;
    }

    public DataType ValueType { get; set; }
    public IList<object> Values => condition.Values;

    public void SetValues(IEnumerable<string> values)
    {
        Values.Clear();

        foreach (var value in values)
        {
            var castValue = DataTypeHelpers.ConvertStringToDataType(value, ValueType);
            Values.Add(castValue);
        }
    }

    public void SetValue(string value, int index)
    {
        Values[index] = DataTypeHelpers.ConvertStringToDataType(value, ValueType);
    }
}