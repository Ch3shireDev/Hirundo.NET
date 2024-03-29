using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Writers.Summary;

namespace Hirundo.Writers.WPF;

public class WritersParametersFactory(IDataLabelRepository repository) : ParametersFactory<IWriterParameters, WritersModel>(repository), IParametersFactory<IWriterParameters>
{
}