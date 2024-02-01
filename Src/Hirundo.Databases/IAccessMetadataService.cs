namespace Hirundo.Databases;

public interface IAccessMetadataService
{
    public IEnumerable<AccessTableMetadata> GetTables(string path);
}