using System.Globalization;
using System.Windows.Data;

namespace Hirundo.Databases.WPF.Helpers;

public class DataSourceParametersHeaderConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return "";

        if (value is Type type)
        {
            if (type == typeof(AccessDatabaseParameters))
            {
                return "Baza danych Access (*.mdb)";
            }
        }

        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}