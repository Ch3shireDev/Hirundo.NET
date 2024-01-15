using Hirundo.Writers.Summary;
using Hirundo.Writers.WPF.CsvWriter;

namespace Hirundo.Writers.WPF;

public static class DataWriterViewModelFactory
{
    public static DataWriterViewModel Create(IWriterParameters writerParameters)
    {
        return writerParameters switch
        {
            CsvSummaryWriterParameters csvWriterConfiguration => new CsvWriterViewModel(new CsvWriterModel(csvWriterConfiguration)),
            _ => throw new ArgumentOutOfRangeException(nameof(writerParameters))
        };
    }
}