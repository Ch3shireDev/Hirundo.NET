using System.Globalization;
using System.Windows.Data;
using Hirundo.Databases.WPF;
using Hirundo.Processors.Observations.WPF;
using Hirundo.Processors.Population.WPF;
using Hirundo.Processors.Returning.WPF;
using Hirundo.Processors.Specimens.WPF;
using Hirundo.Processors.Statistics.WPF;
using Hirundo.Writers.WPF;

namespace Hirundo.App.Helpers;

public class ViewModelToTabItemHeaderConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            DataSourceViewModel => "Źródło danych",
            ObservationsViewModel => "Obserwacje",
            ReturningSpecimensViewModel => "Osobniki powracające",
            PopulationViewModel => "Populacja",
            SpecimensViewModel => "Wszystkie osobniki",
            StatisticsViewModel => "Statystyki",
            WriterViewModel => "Zapis wyników",
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}