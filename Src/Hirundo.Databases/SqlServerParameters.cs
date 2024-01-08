namespace Hirundo.Databases;

public class SqlServerParameters : DatabaseParameters
{
    public string ConnectionString { get; set; } = null!;
    public ColumnMapping[] Columns { get; set; } = [];
}