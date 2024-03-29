using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Writers.Summary;
using Hirundo.Writers.WPF.CsvWriter;

namespace Hirundo.Writers.WPF;

public interface IWritersParametersFactory : IParametersFactory<IWriterParameters>
{
}

public class WritersParametersFactory(IDataLabelRepository repository) : ParametersFactory<IWriterParameters, WritersModel>(repository), IWritersParametersFactory
{
    public static ParametersViewModel Create(IWriterParameters writerParameters)
    {
        return writerParameters switch
        {
            CsvSummaryWriterParameters csvWriterConfiguration => new CsvWriterViewModel(new CsvWriterModel(csvWriterConfiguration)),
            _ => throw new ArgumentOutOfRangeException(nameof(writerParameters))
        };
    }
}