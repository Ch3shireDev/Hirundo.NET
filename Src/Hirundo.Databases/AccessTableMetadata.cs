namespace Hirundo.Databases;

public class AccessTableMetadata
{
    public string TableName { get; init; } = null!;
    public IList<AccessColumnMetadata> Columns { get; init; } = [];
}