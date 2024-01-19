﻿using System.Globalization;
using Hirundo.Commons;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF.IsEqual;

public class IsEqualModel
{
    public IsEqualModel(IsEqualCondition condition)
    {
        OriginalCondition = condition;
        IsEqualCondition = condition;
    }

    public IsEqualCondition OriginalCondition { get; init; }

    public string ValueName { get; set; } = null!;
    public string ValueStr { get; set; } = null!;
    public DataType ValueType { get; set; }

    public IsEqualCondition IsEqualCondition
    {
        get => GetIsEqualFilter();
        set => SetIsEqualFilter(value);
    }

    private object Value
    {
        get => GetValue();
        set => SetValue(value);
    }

    private IsEqualCondition GetIsEqualFilter()
    {
        return new(ValueName, Value);
    }

    private void SetIsEqualFilter(IsEqualCondition value)
    {
        ValueName = value.ValueName;
        ValueStr = value.Value?.ToString() ?? "";

        ValueType = value.Value switch
        {
            string => DataType.Text,
            int => DataType.Number,
            double => DataType.Numeric,
            bool => DataType.Boolean,
            DateTime => DataType.Date,
            _ => DataType.Text
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