using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.CompareValues;

public class CompareValuesReturningModel<TCondition>(TCondition condition, IDataLabelRepository repository) : ParametersModel(condition, repository)
    where TCondition : CompareValuesReturningCondition
{
    public string ValueName
    {
        get => condition.ValueName;
        set => condition.ValueName = value;
    }

    public string ValueStr
    {
        get => DataTypeHelpers.GetValueToString(condition.Value, DataType);
        set => condition.Value = DataTypeHelpers.ConvertStringToDataType(value, DataType);
    }

    public DataType DataType { get; set; }
}