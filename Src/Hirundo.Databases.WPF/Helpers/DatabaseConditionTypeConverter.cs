using Hirundo.Databases.Conditions;
using System.Globalization;
using System.Windows.Data;

namespace Hirundo.Databases.WPF.Helpers
{
    public class DatabaseConditionTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DatabaseConditionType conditionType)
            {
                return ConvertDatabaseConditionType(conditionType);
            }

            return string.Empty;
        }

        private static string ConvertDatabaseConditionType(DatabaseConditionType conditionType)
        {
            return conditionType switch
            {
                DatabaseConditionType.IsGreaterThan => "jest większy niż",
                DatabaseConditionType.IsGreaterOrEqual => "jest większy bądź równy",
                DatabaseConditionType.IsLowerOrEqual => "jest mniejszy bądź równy",
                DatabaseConditionType.IsLowerThan => "jest mniejszy niż",
                DatabaseConditionType.IsEqual => "jest równy",
                _ => throw new ArgumentOutOfRangeException(nameof(conditionType), conditionType, null),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
