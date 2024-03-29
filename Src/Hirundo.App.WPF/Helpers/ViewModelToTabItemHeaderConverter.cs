using Hirundo.Commons.WPF;
using Hirundo.Processors.Specimens.WPF;
using System.Globalization;
using System.Windows.Data;

namespace Hirundo.App.WPF.Helpers;

public class ViewModelToTabItemHeaderConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            ParametersBrowserViewModel viewModel => viewModel.Header,
            SpecimensViewModel => "Osobniki",
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}