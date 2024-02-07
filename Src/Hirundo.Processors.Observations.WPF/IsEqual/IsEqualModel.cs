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
        return ValueType switch
        {
            DataType.Text => Condition.Value as string ?? string.Empty,
            DataType.Number => Condition.Value is int intValue ? intValue.ToString(CultureInfo.InvariantCulture) : string.Empty,
            DataType.Numeric => Condition.Value is double doubleValue ? doubleValue.ToString(CultureInfo.InvariantCulture) : string.Empty,
            DataType.Date => Condition.Value is DateTime dateValue ? dateValue.ToString(CultureInfo.InvariantCulture) : string.Empty,
            DataType.Boolean => Condition.Value is bool boolValue ? boolValue.ToString(CultureInfo.InvariantCulture) : string.Empty,
            DataType.Undefined => Condition.Value as string ?? string.Empty,
            _ => string.Empty
        };
    }


    private void SetValueFromString(string value)
    {
        Condition.Value = DataType switch
        {
            DataType.Text => value,
            DataType.Number when int.TryParse(value, out var intValue) => intValue,
            DataType.Number => value,
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