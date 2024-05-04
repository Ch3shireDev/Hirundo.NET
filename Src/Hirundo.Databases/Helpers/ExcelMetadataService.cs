
namespace Hirundo.Databases.Helpers;

public class ExcelMetadataService : IExcelMetadataService
{
    public IList<ColumnParameters> GetColumns(string path)
    {
        var columnNames = new[] { "ID", "RING", "SPECIES", "DATE", "HOUR", "SEX", "AGE", "WEIGHT", "STATUS", "FAT" };

        return [

                new ColumnParameters { DatabaseColumn = "ID", ValueName = "ID" },
                new ColumnParameters { DatabaseColumn = "RING", ValueName = "RING" },
                new ColumnParameters { DatabaseColumn = "SPECIES", ValueName = "SPECIES" },
                new ColumnParameters { DatabaseColumn = "DATE", ValueName = "DATE" },
                new ColumnParameters { DatabaseColumn = "HOUR", ValueName = "HOUR" },
                new ColumnParameters { DatabaseColumn = "SEX", ValueName="SEX"},
                new ColumnParameters { DatabaseColumn = "AGE", ValueName="AGE"},
                new ColumnParameters { DatabaseColumn = "WEIGHT", ValueName="WEIGHT"},
                new ColumnParameters { DatabaseColumn = "STATUS", ValueName="STATUS"},
                new ColumnParameters { DatabaseColumn = "FAT", ValueName="FAT"}

            ];
    }
}
