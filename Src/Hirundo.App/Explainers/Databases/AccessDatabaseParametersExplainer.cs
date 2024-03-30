using Hirundo.Commons.Helpers;
using Hirundo.Databases;
using Hirundo.Databases.Conditions;
using System.Globalization;
using System.Text;

namespace Hirundo.App.Explainers.Databases;

public class AccessDatabaseParametersExplainer : ParametersExplainer<AccessDatabaseParameters>
{
    public string DataSourceInfo { get; set; } = "Pobiera dane z pliku {0}, z tabeli {1}.";
    public string ColumnInfo { get; set; } = "Zapisuje kolumnę {0} jako {1}, typu {2}.";

    public override string Explain(AccessDatabaseParameters config)
    {
        ArgumentNullException.ThrowIfNull(config, nameof(config));

        var sb = new StringBuilder();

        sb.AppendLine("Konfiguracja bazy danych Access:");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, DataSourceInfo, config.Path, config.Table));
        sb.AppendLine();
        sb.AppendLine("Kolumny:");
        foreach (var column in config.Columns)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, ColumnInfo, column.DatabaseColumn, column.ValueName,
                ExplainDataType(column.DataType
                )
                ));
        }
        sb.AppendLine();
        sb.AppendLine("Warunki:");
        foreach (var condition in config.Conditions)
        {
            sb.AppendLine($"Kolumna {condition.DatabaseColumn} {GetOperationName(condition.Type)} {condition.Value}");
        }

        return sb.ToString();
    }

    private static string GetOperationName(DatabaseConditionType condition) => condition switch
    {
        DatabaseConditionType.IsEqual => "równa się",
        DatabaseConditionType.IsNotEqual => "nie równa się",
        DatabaseConditionType.IsGreaterThan => "jest większa niż",
        DatabaseConditionType.IsGreaterOrEqual => "jest większa lub równa",
        DatabaseConditionType.IsLowerThan => "jest mniejsza niż",
        DatabaseConditionType.IsLowerOrEqual => "jest mniejsza lub równa",
        _ => throw new NotImplementedException()
    };

    private static string ExplainDataType(DataValueType dataType) => dataType switch
    {
        DataValueType.LongInt => "duża liczba całkowita",
        DataValueType.Text => "tekst",
        DataValueType.ShortInt => "mała liczba całkowita",
        DataValueType.Undefined => "nieokreślony",
        DataValueType.DateTime => "data",
        DataValueType.Numeric => "liczba wymierna",
        _ => throw new NotImplementedException()
    };
}
