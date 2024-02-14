using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.IsEqual;

public class IsEqualReturningModel(IsEqualReturningCondition condition, IDataLabelRepository repository)
{
    public IsEqualReturningCondition Condition
    {
        get => condition;
        set => condition = value;
    }

    public string ValueName
    {
        get => Condition.ValueName;
        set => Condition.ValueName = value;
    }

    public string ValueStr
    {
        get => DataTypeHelpers.GetValueToString(Condition.Value, DataType);
        set => Condition.Value = DataTypeHelpers.ConvertStringToDataType(value, DataType);
    }

    public DataType DataType { get; set; }
    public IDataLabelRepository Repository { get; } = repository;
}