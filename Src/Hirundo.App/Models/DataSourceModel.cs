using Hirundo.Databases;

namespace Hirundo.App.Models;

public class DataSourceModel
{
    public IList<IDatabaseParameters> DatabaseParameters { get; set; } = new List<IDatabaseParameters>();
}