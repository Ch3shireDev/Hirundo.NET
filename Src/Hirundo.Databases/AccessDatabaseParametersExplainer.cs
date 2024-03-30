using Hirundo.Commons;
using System.Globalization;
using System.Text;

namespace Hirundo.Databases;

public class AccessDatabaseParametersExplainer : ParametersExplainer<AccessDatabaseParameters>
{
    public string DataSourceInfo { get; set; } = "Pobiera dane z pliku {0}, z tabeli {1}.";
    public string ColumnInfo { get; set; } = "Zapisuje kolumnę {0} jako {1}, typu {2}.";

    public override string Explain(AccessDatabaseParameters config)
    {
        ArgumentNullException.ThrowIfNull(config, nameof(config));

        var sb = new StringBuilder();

        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, DataSourceInfo, config.Path, config.Table));
        foreach (var column in config.Columns)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, ColumnInfo, column.DatabaseColumn, column.ValueName,
                ExplainDataType(column.DataType
                )
                ));
        }

        return sb.ToString();
    }

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