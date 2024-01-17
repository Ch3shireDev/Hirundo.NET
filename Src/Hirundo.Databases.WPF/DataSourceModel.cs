namespace Hirundo.Databases.WPF;

public class DataSourceModel
{
    public IList<IDatabaseParameters> DatabaseParameters { get; } = new List<IDatabaseParameters>();

    public void AddDataSource(Type selectedDataSourceType)
    {
        switch (selectedDataSourceType)
        {
            case { } accessDatabaseParametersType when accessDatabaseParametersType == typeof(AccessDatabaseParameters):
                DatabaseParameters.Add(new AccessDatabaseParameters());
                break;
            default:
                throw new NotImplementedException();
        }
    }
}