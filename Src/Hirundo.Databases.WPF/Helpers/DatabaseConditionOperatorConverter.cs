using System.Globalization;
using System.Windows.Data;
using Hirundo.Databases.Conditions;

namespace Hirundo.Databases.WPF.Helpers;

public class DatabaseConditionOperatorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DatabaseConditionOperator conditionOperator)
        {
            return ConvertConditionOperator(conditionOperator);
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private static string ConvertConditionOperator(DatabaseConditionOperator conditionOperator)
    {
        return conditionOperator switch
        {
            DatabaseConditionOperator.And => "Oraz",
            DatabaseConditionOperator.Or => "Lub",
            _ => throw new ArgumentOutOfRangeException(nameof(conditionOperator), conditionOperator, null)
        };
    }
}