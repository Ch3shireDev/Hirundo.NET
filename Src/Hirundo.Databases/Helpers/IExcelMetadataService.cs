namespace Hirundo.Databases.Helpers;

public interface IExcelMetadataService
{
    IList<ColumnParameters> GetColumns(string path);
    IList<string> GetDistinctValues(string path, string speciesColumn);
}