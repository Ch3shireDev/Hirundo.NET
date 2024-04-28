using System.Globalization;
using System.Windows.Data;
using Hirundo.Commons.WPF;

namespace Hirundo.App.WPF.Helpers;

public class ViewModelToTabItemHeaderConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            ParametersBrowserViewModel viewModel => viewModel.Header,
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}