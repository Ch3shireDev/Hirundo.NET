using System.Globalization;
using Hirundo.Commons;
using Hirundo.Processors.Observations.Conditions;
using Hirundo.Repositories.DataLabels;

namespace Hirundo.Processors.Observations.WPF.IsEqual;

public class IsEqualModel
{
    private readonly IDataLabelRepository _repository;


    private DataLabel? _selectedLabel;

    public IsEqualModel(IsEqualCondition condition, IDataLabelRepository repository)
    {
        OriginalCondition = condition;
        IsEqualCondition = condition;
        _repository = repository;
    }

    public IsEqualCondition OriginalCondition { get; init; }

    public string ValueName
    {
        get => OriginalCondition.ValueName;
        set => OriginalCondition.ValueName = value;
    }

    public string ValueStr { get; set; } = null!;
    public DataType ValueType { get; set; }

    public IsEqualCondition IsEqualCondition
    {
        get => GetIsEqualCondition();
        set => SetIsEqualCondition(value);
    }

    private object Value
    {
        get => GetValue();
        set => SetValue(value);
    }


    public IList<DataLabel> Labels => [.._repository.GetLabels()];

    public DataLabel? SelectedLabel
    {
        get => _selectedLabel;
        set
        {
            _selectedLabel = value;
            ValueName = value?.Name ?? "";
        }
    }

    private IsEqualCondition GetIsEqualCondition()
    {
        var name = SelectedLabel?.Name ?? ValueName;
        return new(name, Value);
    }

    private void SetIsEqualCondition(IsEqualCondition value)
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