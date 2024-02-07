using System.Globalization;

namespace Hirundo.Commons.WPF;

public class ValueContainer : ViewModelBase
{
    private string value = string.Empty;

    public ValueContainer()
    {
    }

    public ValueContainer(object? value)
    {
        Value = value?.ToString() ?? string.Empty;
    }

    public ValueContainer(string? value)
    {
        Value = value ?? string.Empty;
    }

    public string Value
    {
        get => value;
        set
        {
            this.value = value;
            OnPropertyChanged();
        }
    }

    public static implicit operator string(ValueContainer valueContainer)
    {
        return valueContainer.Value;
    }

    public static implicit operator ValueContainer(string value)
    {
        return new ValueContainer(value);
    }

    public static implicit operator ValueContainer(int value)
    {
        return new ValueContainer(value.ToString());
    }

    public static implicit operator ValueContainer(double value)
    {
        return new ValueContainer(value.ToString(CultureInfo.InvariantCulture));
    }

    public static implicit operator ValueContainer(bool value)
    {
        return new ValueContainer(value.ToString());
    }

    public static implicit operator ValueContainer(DateTime value)
    {
        return new ValueContainer(value.ToString("yyyy-MM-dd"));
    }
}