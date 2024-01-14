using Hirundo.App.Components.DataSource.Access;
using Hirundo.Databases;

namespace Hirundo.App.Components.DataSource;

public class DataSourceViewModel : ViewModelBase
{
    public DataSourceViewModel(DataSourceModel model)
    {
        DatabaseViewModels = model.DatabaseParameters.Select(x => new AccessDataSourceViewModel(x as AccessDatabaseParameters)).ToList();
    }

    public IList<AccessDataSourceViewModel> DatabaseViewModels { get; set; } = [];
}