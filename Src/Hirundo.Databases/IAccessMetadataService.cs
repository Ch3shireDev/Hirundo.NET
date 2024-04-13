namespace Hirundo.Databases;

public interface IAccessMetadataService
{
    IEnumerable<object?> GetDistinctValues(string path, string table, string columnName);
    IEnumerable<AccessTableMetadata> GetTables(string path);
}