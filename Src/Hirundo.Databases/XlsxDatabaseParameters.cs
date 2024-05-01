using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;
using System.Globalization;
using System.Text;

namespace Hirundo.Databases;

[TypeDescription("Xlsx", "Źródło danych Excel", "Źródło danych z pliku arkusza Excel (.xlsx).")]
public class XlsxDatabaseParameters : IDatabaseParameters, IFileSource, ISelfExplainer
{
    public string Path { get; set; } = string.Empty;
    public string RingIdentifier { get; set; } = string.Empty;
    public string DateIdentifier { get; set; } = string.Empty;
    public string SpeciesIdentifier { get; set; } = string.Empty;
    public IList<ColumnParameters> Columns { get; init; } = [];

    public string Explain()
    {
        var sb = new StringBuilder();
        sb.AppendLine(CultureInfo.InvariantCulture, $"Dane pobrano z pliku {Path}. Kolumna obrączki: {RingIdentifier}, kolumna daty: {DateIdentifier}. Liczba kolumn: {Columns.Count}.");
        sb.AppendLine("Kolumny:");
        foreach (var column in Columns)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, " - Kolumna {0} jako {1}, typu {2}.", column.DatabaseColumn, column.ValueName, column.DataType));
        }

        return sb.ToString();
    }
}
