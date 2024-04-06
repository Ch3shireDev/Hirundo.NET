using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.IsInSet;

public class IsInSetModel(IsInSetCondition condition, ILabelsRepository repository) : ParametersModel(condition, repository)
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