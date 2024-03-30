using Hirundo.Commons.Helpers;
using System.Globalization;
using System.Text;

namespace Hirundo.Databases;

public class DatabaseConfigExplainer : ParametersExplainer<DatabaseParameters>
{
    public string Header { get; set; } = "Konfiguracja źródła danych";
    public string SubheaderText { get; set; } = "Liczba źródeł danych: {0}.";

    public string DataSourceInfo { get; set; } = "Pobiera dane z pliku {0}, z tabeli {1}.";

    public override string Explain(DatabaseParameters @params)
    {
        ArgumentNullException.ThrowIfNull(@params, nameof(@params));

        var sb = new StringBuilder();
        sb.AppendLine(Header);
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, SubheaderText, @params.Databases.Count));

        foreach (var database in @params.Databases)
        {
            if (database is AccessDatabaseParameters accessDatabase)
            {
                var accessDatabaseConfigExplainer = new AccessDatabaseParametersExplainer();
                sb.AppendLine(accessDatabaseConfigExplainer.Explain(accessDatabase));
            }
        }


        return sb.ToString();
    }
}
