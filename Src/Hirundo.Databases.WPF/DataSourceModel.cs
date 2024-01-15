namespace Hirundo.Databases.WPF;

public class DataSourceModel
{
    public IList<IDatabaseParameters> DatabaseParameters { get; } = new List<IDatabaseParameters>();
}