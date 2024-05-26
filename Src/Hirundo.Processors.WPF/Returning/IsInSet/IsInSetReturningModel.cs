using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.WPF.Returning.IsInSet;

public class IsInSetReturningModel(IsInSetReturningCondition condition, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : ParametersModel(condition, labelsRepository, speciesRepository)
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