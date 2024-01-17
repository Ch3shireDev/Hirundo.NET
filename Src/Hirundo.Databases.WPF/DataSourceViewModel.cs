using Hirundo.Commons.WPF;
using Hirundo.Databases.WPF.Access;

namespace Hirundo.Databases.WPF;

public class DataSourceViewModel : ViewModelBase
{
    public DataSourceViewModel(DataSourceModel model)
    {
        DatabaseViewModels = model.DatabaseParameters.Select(x => new AccessDataSourceViewModel(x as AccessDatabaseParameters)).ToList();
    }

    public string Header { get; set; } = "twoja stara";
    public IList<AccessDataSourceViewModel> DatabaseViewModels { get; set; } = [];
}