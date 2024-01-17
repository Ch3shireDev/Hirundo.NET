using System.Globalization;
using System.Windows.Data;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population.WPF.Helpers;

internal class PopulationConditionToStringConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Type type)
        {
            return type.Name switch
            {
                nameof(IsInSharedTimeWindowFilterBuilder) => "Czy jest we współdzielonym oknie czasowym?",
                _ => throw new ArgumentException($"Unknown type: {type.Name}")
            };
        }

        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}