using System.Globalization;
using System.Windows.Data;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF.Helpers;

internal class ObservationToStringConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Type type)
        {
            return type.Name switch
            {
                nameof(IsEqualFilter) => "Czy jest równy?",
                nameof(IsInSeasonFilter) => "Czy jest w sezonie?",
                nameof(IsInTimeBlockFilter) => "Czy jest w przedziale czasowym?",
                _ => "Unknown"
            };
        }

        return "";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}