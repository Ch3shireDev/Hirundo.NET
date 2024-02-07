using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF.IsInSet;

public class IsInSetModel(IsInSetCondition condition, IDataLabelRepository repository)
{
    public IDataLabelRepository Repository { get; } = repository;
    public IsInSetCondition Condition { get; init; } = condition;

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
            var castValue = CastToDataType(value, ValueType);
            Values.Add(castValue);
        }
    }

    public void SetValue(string value, int index)
    {
        var castValue = CastToDataType(value, ValueType);
        Values[index] = castValue;
    }

    private static object CastToDataType(string valueStr, DataType valueType)
    {
        switch (valueType)
        {
            case DataType.Number:
                var valueInt = int.TryParse(valueStr, out var intValue) ? intValue : 0;
                return valueInt;
            case DataType.Numeric:
                var valueDouble = double.TryParse(valueStr, out var doubleValue) ? doubleValue : 0;
                return valueDouble;
            case DataType.Date:
                var valueDate = DateTime.TryParse(valueStr, out var dateValue) ? dateValue : DateTime.MinValue;
                return valueDate;
            case DataType.Boolean:
                var valueBool = bool.TryParse(valueStr, out var boolValue) && boolValue;
                return valueBool;
            case DataType.Text:
                return valueStr;
            case DataType.Undefined:
                return valueStr;
            default:
                throw new ArgumentOutOfRangeException(nameof(valueType));
        }
    }
}