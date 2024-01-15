using System.Globalization;
using Hirundo.Commons;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.App.Components.Observations.IsEqual;

internal class IsEqualModel
{
    public IsEqualModel()
    {
    }

    public IsEqualModel(IsEqualFilter filter)
    {
        IsEqualFilter = filter;
    }

    public string ValueName { get; set; } = null!;
    public string ValueStr { get; set; } = null!;
    public DataType ValueType { get; set; }

    public IsEqualFilter IsEqualFilter
    {
        get => GetIsEqualFilter();
        set => SetIsEqualFilter(value);
    }

    private object Value
    {
        get => GetValue();
        set => SetValue(value);
    }

    private IsEqualFilter GetIsEqualFilter()
    {
        return new(ValueName, Value);
    }

    private void SetIsEqualFilter(IsEqualFilter value)
    {
        ValueName = value.ValueName;
        ValueStr = value.Value.ToString()!;

        ValueType = value.Value switch
        {
            string => DataType.Text,
            int => DataType.Number,
            double => DataType.Numeric,
            bool => DataType.Boolean,
            DateTime => DataType.Date,
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }

    private object GetValue()
    {
        return ValueType switch
        {
            DataType.Text => ValueStr,
            DataType.Number => int.Parse(ValueStr, CultureInfo.InvariantCulture),
            DataType.Numeric => double.Parse(ValueStr, CultureInfo.InvariantCulture),
            DataType.Boolean => bool.Parse(ValueStr),
            DataType.Date => DateTime.Parse(ValueStr, CultureInfo.InvariantCulture),
            _ => throw new ArgumentOutOfRangeException(nameof(ValueType))
        };
    }

    private void SetValue(object value)
    {
        ValueStr = value.ToString()!;
        ValueType = value switch
        {
            string => DataType.Text,
            int => DataType.Number,
            double => DataType.Numeric,
            bool => DataType.Boolean,
            DateTime => DataType.Date,
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }
}