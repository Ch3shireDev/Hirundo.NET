using System.Data;
using System.Data.Odbc;

namespace Hirundo.Databases;

public class AccessMetadataService : IAccessMetadataService
{
    private readonly Dictionary<string, AccessTableMetadata[]> _accessMetadata = [];

    public IEnumerable<AccessTableMetadata> GetTables(string path)
    {
        if (_accessMetadata.TryGetValue(path, out var metadata))
        {
            if (metadata.Length > 0)
            {
                return metadata;
            }
        }

        // Replace with your actual database file path and connection string details
        var connectionString = $"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={path};";

        using var connection = new OdbcConnection(connectionString);

        // Open the connection
        connection.Open();

        // Get the list of table names from the schema
        var tables = connection.GetSchema("Tables");

        var result = GetAccessTableMetadatas(tables, connection).ToArray();

        _accessMetadata[path] = result;

        return result;
    }
    public IEnumerable<object?> GetDistinctValues(string path, string table, string columnName)
    {
        // Replace with your actual database file path and connection string details
        var connectionString = $"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={path};";

        using var connection = new OdbcConnection(connectionString);

        // Open the connection
        connection.Open();

        // make query for distinct values of column

#pragma warning disable CA2100
        var command = $"SELECT DISTINCT [{columnName}] FROM [{table}]";
        using var query = new OdbcCommand(command, connection);
#pragma warning restore CA2100

        using var reader = query.ExecuteReader();

        while (reader.Read())
        {
            yield return reader[0];
        }
    }

    private static IEnumerable<AccessTableMetadata> GetAccessTableMetadatas(DataTable tables, OdbcConnection connection)
    {
        foreach (DataRow row in tables.Rows)
        {
            var tableName = row["TABLE_NAME"].ToString();

            if (tableName == null) continue;
            if (tableName.StartsWith("MSys", StringComparison.InvariantCultureIgnoreCase)) continue;

            var columns = GetColumns(connection, tableName).ToArray();

            yield return new AccessTableMetadata
            {
                TableName = tableName,
                Columns = columns
            };
        }
    }

    private static IEnumerable<AccessColumnMetadata> GetColumns(OdbcConnection connection, string tableName)
    {
        var columns = connection.GetSchema("Columns", [null, null, tableName, null]);

        foreach (DataRow row in columns.Rows)
        {
            var columnName = row["COLUMN_NAME"].ToString() ?? "";
            var typeName = row["TYPE_NAME"].ToString() ?? "";
            var dataType = (short)row["DATA_TYPE"];
            var sqlDataType = (short)row["SQL_DATA_TYPE"];
            var ordinalPosition = (int)row["ORDINAL_POSITION"];
            var isNullable = (short)row["NULLABLE"] == 1;

            yield return new AccessColumnMetadata
            {
                OrdinalPosition = ordinalPosition,
                ColumnName = columnName,
                TypeName = typeName,
                DataType = dataType,
                IsNullable = isNullable,
                SqlDataType = sqlDataType
            };
        }
    }

}