using Hirundo.Commons.Helpers;
using Hirundo.Databases;
using System.Globalization;
using System.Text;

namespace Hirundo.App.Explainers.Databases;

public class DatabaseParametersExplainer : ParametersExplainer<DatabaseParameters>
{
    public string Header { get; set; } = "Konfiguracja źródła danych:";
    public string SubheaderText { get; set; } = "Liczba źródeł danych: {0}.";

    public override string Explain(DatabaseParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));

        var sb = new StringBuilder();
        sb.AppendLine(Header);
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, SubheaderText, parameters.Databases.Count));

        sb.AppendLine();

        foreach (var database in parameters.Databases)
        {
            sb.AppendLine(ExplainerHelpers.Explain(database));
        }

        return sb.ToString();
    }
}