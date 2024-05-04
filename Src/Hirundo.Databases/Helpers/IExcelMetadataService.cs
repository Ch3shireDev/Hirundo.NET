namespace Hirundo.Databases.Helpers;

public interface IExcelMetadataService
{
    IList<ColumnParameters> GetColumns(string path);
}