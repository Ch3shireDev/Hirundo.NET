using System.Globalization;
using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF.IsEqual;

public class IsEqualModel(IsEqualCondition condition, IDataLabelRepository repository)
{
    public IsEqualCondition Condition { get; init; } = condition;

    public string ValueName
    {
        get => Condition.ValueName;
        set => Condition.ValueName = value;
    }

    public string ValueStr
    {
        get => GetValue();
        set => SetValueFromString(value);
    }

    public DataType ValueType { get; set; }

    public IDataLabelRepository Repository { get; } = repository;
    public DataType DataType { get; set; }

    private string GetValue()
    {
        switch (ValueType)
        {
            case DataType.Text:
                return Condition.Value as string ?? string.Empty;
            case DataType.Integer:
                return Condition.Value is int intValue ? intValue.ToString(CultureInfo.InvariantCulture) : string.Empty;
            case DataType.Numeric:
                return Condition.Value is double doubleValue ? doubleValue.ToString(CultureInfo.InvariantCulture) : string.Empty;
            case DataType.Date:
                return Condition.Value is DateTime dateValue ? dateValue.ToString(CultureInfo.InvariantCulture) : string.Empty;
            case DataType.Boolean:
                return Condition.Value is bool boolValue ? boolValue.ToString(CultureInfo.InvariantCulture) : string.Empty;
            case DataType.Undefined:
                return Condition.Value as string ?? string.Empty;
            default:
                return string.Empty;
        }
    }


    private void SetValueFromString(string value)
    {
        Condition.Value = DataType switch
        {
            DataType.Text => value,
            DataType.Integer when int.TryParse(value, out var intValue) => intValue,
            DataType.Integer => value,
            DataType.Numeric when double.TryParse(value, out var doubleValue) => doubleValue,
            DataType.Numeric => value,
            DataType.Date when DateTime.TryParse(value, out var dateValue) => dateValue,
            DataType.Date => value,
            DataType.Boolean when bool.TryParse(value, out var boolValue) => boolValue,
            DataType.Boolean => value,
            DataType.Undefined => value,
            _ => Condition.Value
        };
    }
}