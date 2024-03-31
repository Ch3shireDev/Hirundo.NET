using Microsoft.Win32;

namespace Hirundo.Writers.WPF;
public static class FileDialogFactory
{
    public static SaveFileDialog GetFileDialogForWriter(IWriterParameters writerParameters)
    {
        if (writerParameters is CsvSummaryWriterParameters)
        {
            return new SaveFileDialog
            {
                Filter = "Pliki CSV (*.csv)|*.csv",
                Title = "Zapisz wyniki",
                DefaultExt = "csv",
            };
        }
        else if (writerParameters is XlsxSummaryWriterParameters)
        {
            return new SaveFileDialog
            {
                Filter = "Pliki Excel (*.xlsx)|*.xlsx",
                Title = "Zapisz wyniki",
                DefaultExt = "xlsx",
            };
        }
        else if (writerParameters is ExplanationWriterParameters)
        {
            return new SaveFileDialog
            {
                Filter = "Pliki tekstowe (*.txt)|*.txt",
                Title = "Zapisz wyjaśnienia",
                DefaultExt = "txt",
            };
        }
        else
        {
            throw new ArgumentException("Unsupported writer parameters type");
        }
    }
}
