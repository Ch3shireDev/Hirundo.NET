using System.IO;
using Hirundo.Commons.Helpers;
using Microsoft.Win32;

namespace Hirundo.Writers.WPF;

public static class FileDialogFactory
{
    public static string DefaultResultsName { get; set; } = "wyniki";
    public static string DefaultConfigName { get; set; } = "konfiguracja";
    public static string DefaultDirectory { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

    public static SaveFileDialog GetFileDialogForWriter(IWriterParameters writerParameters)
    {
        if (writerParameters is CsvSummaryWriterParameters)
        {
            return new SaveFileDialog
            {
                Filter = "Pliki CSV (*.csv)|*.csv",
                Title = "Zapisz wyniki",
                DefaultExt = "csv",
                FileName = GetDefaultFilename($"{DefaultResultsName}.csv"),
                DefaultDirectory = DefaultDirectory
            };
        }

        if (writerParameters is XlsxSummaryWriterParameters)
        {
            return new SaveFileDialog
            {
                Filter = "Pliki Excel (*.xlsx)|*.xlsx",
                Title = "Zapisz wyniki",
                DefaultExt = "xlsx",
                FileName = GetDefaultFilename($"{DefaultResultsName}.xlsx"),
                DefaultDirectory = DefaultDirectory
            };
        }

        if (writerParameters is ExplanationWriterParameters)
        {
            return new SaveFileDialog
            {
                Filter = "Pliki tekstowe (*.txt)|*.txt",
                Title = "Zapisz wyjaśnienia",
                DefaultExt = "txt",
                FileName = GetDefaultFilename("wyjasnienia.txt"),
                DefaultDirectory = DefaultDirectory
            };
        }

        throw new ArgumentException("Unsupported writer parameters type");
    }

    public static SaveFileDialog GetFileDialogForFilename(string filename)
    {
        var extension = Path.GetExtension(filename);

        if (extension.Equals(".csv", StringComparison.InvariantCultureIgnoreCase))
        {
            return new SaveFileDialog
            {
                Filter = "Pliki CSV (*.csv)|*.csv",
                Title = "Zapisz wyniki",
                DefaultExt = extension,
                FileName = GetDefaultFilename($"{DefaultResultsName}{extension}"),
                DefaultDirectory = DefaultDirectory
            };
        }

        if (extension.Equals(".xlsx", StringComparison.InvariantCultureIgnoreCase))
        {
            return new SaveFileDialog
            {
                Filter = "Pliki Excel (*.xlsx)|*.xlsx",
                Title = "Zapisz wyniki",
                DefaultExt = extension,
                FileName = GetDefaultFilename($"{DefaultResultsName}{extension}"),
                DefaultDirectory = DefaultDirectory
            };
        }

        if (extension.Equals(".txt", StringComparison.InvariantCultureIgnoreCase))
        {
            return new SaveFileDialog
            {
                Filter = "Pliki tekstowe (*.txt)|*.txt",
                Title = "Zapisz wyjaśnienia",
                DefaultExt = extension,
                FileName = GetDefaultFilename("wyjasnienia.txt"),
                DefaultDirectory = DefaultDirectory
            };
        }

        if (extension.Equals(".json", StringComparison.InvariantCultureIgnoreCase))
        {
            return new SaveFileDialog
            {
                Filter = "Pliki JSON (*.json)|*.json",
                Title = "Zapisz konfigurację",
                DefaultExt = extension,
                FileName = GetDefaultFilename($"{DefaultConfigName}{extension}"),
                DefaultDirectory = DefaultDirectory
            };
        }

        throw new ArgumentException("Unsupported file extension");
    }


    private static string GetDefaultFilename(string fileName)
    {
        return FileHelpers.GetUniqueFilename(fileName, DefaultDirectory);
    }
}