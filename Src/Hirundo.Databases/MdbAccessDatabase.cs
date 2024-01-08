using System.Data.Odbc;
using Hirundo.Commons;
using Serilog;

namespace Hirundo.Databases;

/// <summary>
///     Klasa reprezentująca połączenie z bazą danych Access. Poprzez parametry wejściowe definiowana jest nazwa pliku,
///     nazwa tabeli oraz zestaw kolumn do pobrania.
/// </summary>
/// <param name="parameters"></param>
public class MdbAccessDatabase(AccessDatabaseParameters parameters) : IDatabase
{
    private readonly MdbAccessQueryBuilder _queryBuilder = new();

    /// <summary>
    ///     ConnectionString jest tworzony z użyciem Microsoft Access Driver dla połączenia ODBC.
    /// </summary>
    private string ConnectionString => $"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={parameters.Path};";

    /// <summary>
    ///     Dane są zwracane na podstawie parametrów wejściowych podanych w konstruktorze klasy.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Observation> GetObservations()
    {
        using var connection = new OdbcConnection(ConnectionString);

        connection.Open();

        var query = _queryBuilder
            .WithTable(parameters.Table)
            .WithColumns(parameters.Columns)
            .Build();

        using var command = new OdbcCommand(query, connection);
        using var reader = command.ExecuteReader();

        var dataColumns = parameters.Columns.Select(x => x.ValueName).ToArray();

        Log.Information($"Odczytywanie danych z tabeli {parameters.Table}, z kolumn: {
            string.Join(", ", parameters.Columns.Select(c => c.DatabaseColumn))
        }");

        while (reader.Read())
        {
            var dataValues = GetValuesFromReader(reader);
            yield return new Observation(dataColumns, dataValues);
        }

        Log.Information("Zakończono odczyt danych z bazy danych Access.");
    }

    /// <summary>
    ///     Wartości są zwracane w postaci tablicy obiektów, bez względu na nazwy kolumn i typ danych.
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    private static object[] GetValuesFromReader(OdbcDataReader reader)
    {
        var columnCount = reader.FieldCount;
        var array = new object[columnCount];
        reader.GetValues(array);
        return array;
    }
}