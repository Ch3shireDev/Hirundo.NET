using Hirundo.Commons.Helpers;
using System.Globalization;
using System.Text;

namespace Hirundo.Databases;

public class DatabaseParameters : ISelfExplainer
{
    private readonly string Header = "Konfiguracja źródła danych:";
    private readonly string SubheaderText = "Liczba źródeł danych: {0}.";
    public IList<IDatabaseParameters> Databases { get; init; } = [];

    public IDatabase BuildDataSource(CancellationToken? token = null)
    {
        return new DatabaseBuilder()
            .WithDatabaseParameters([.. Databases])
            .WithCancellationToken(token)
            .Build();
    }

    public string Explain()
    {
        var sb = new StringBuilder();
        sb.AppendLine(Header);
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, SubheaderText, Databases.Count));

        sb.AppendLine();

        foreach (var database in Databases)
        {
            sb.AppendLine(ExplainerHelpers.Explain(database));
        }

        return sb.ToString();
    }
}