using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;

namespace Hirundo.Databases.WPF;

public interface ILabelsUpdater
{
    event EventHandler<EventArgs>? LabelsUpdated;
}


public interface IDatabaseParametersFactory : IParametersFactory<IDatabaseParameters>
{
}
public class DatabaseParametersFactory(IDataLabelRepository repository) : ParametersFactory<IDatabaseParameters, DataSourceModel>(repository), IDatabaseParametersFactory
{
}