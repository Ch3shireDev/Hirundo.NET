using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;

namespace Hirundo.Writers.WPF.CsvWriter;

public class CsvWriterModel(CsvSummaryWriterParameters parameters, IDataLabelRepository repository = null!) : ParametersModel(parameters, repository)
{
    public string Path
    {
        get => parameters.Path;
        set => parameters.Path = value;
    }
}