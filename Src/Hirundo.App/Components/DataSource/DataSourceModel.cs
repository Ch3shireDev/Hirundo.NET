using Hirundo.Databases;

namespace Hirundo.App.Components.DataSource;

public class DataSourceModel
{
    public IList<IDatabaseParameters> DatabaseParameters { get; } = new List<IDatabaseParameters>();
}