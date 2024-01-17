using Hirundo.Commons.WPF;
using Hirundo.Databases.WPF.Access;

namespace Hirundo.Databases.WPF;

public static class DataSourceViewModelFactory
{
    public static ViewModelBase Create(IDatabaseParameters parameters)
    {
        return parameters switch
        {
            AccessDatabaseParameters accessDatabaseParameters => new AccessDataSourceViewModel(accessDatabaseParameters),
            _ => throw new NotImplementedException()
        };
    }
}