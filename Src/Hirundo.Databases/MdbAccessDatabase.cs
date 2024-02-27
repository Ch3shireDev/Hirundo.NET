using Hirundo.Commons;
using Serilog;
using System.Data.Odbc;

namespace Hirundo.Databases;

/// <summary>
///     Klasa reprezentująca połączenie z bazą danych Access. Poprzez parametry wejściowe definiowana jest nazwa pliku,
///     nazwa tabeli oraz zestaw kolumn do pobrania.
/// </summary>
/// <param name="parameters"></param>
public class MdbAccessDatabase(AccessDatabaseParameters parameters, CancellationToken? token = null) : IDatabase
{
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
        Log.Information("Nawiązywanie połączenia z bazą danych Access.");
        using var connection = new OdbcConnection(ConnectionString);

        connection.Open();

        var query = new MdbAccessQueryBuilder("\n")
            .WithTable(parameters.Table)
            .WithColumns(parameters.Columns)
            .WithConditions(parameters.Conditions)
            .Build();

        Log.Debug($"Access query: {query}");

#pragma warning disable CA2100
        using var command = new OdbcCommand(query, connection);
#pragma warning restore CA2100
        using var reader = command.ExecuteReader();

        var dataColumns = parameters.Columns.Select(x => x.ValueName).ToArray();

        Log.Information($"Odczytywanie danych z tabeli {parameters.Table}.");

        while (reader.Read())
        {
            var dataValues = GetValuesFromReader(reader);
            yield return new Observation(dataColumns, dataValues);

            token?.ThrowIfCancellationRequested();
        }

        Log.Information("Zakończono odczyt danych z bazy danych Access.");
    }

    /// <summary>
    ///     Wartości są zwracane w postaci tablicy obiektów, bez względu na nazwy kolumn i typ danych.
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    private static object?[] GetValuesFromReader(OdbcDataReader reader)
    {
        var columnCount = reader.FieldCount;
        object?[] array = new object[columnCount];
        reader.GetValues(array);
        NullifyElements(array, columnCount);
        return array;
    }

    private static void NullifyElements(object?[] array, int columnCount)
    {
        for (var i = 0; i < columnCount; i++)
        {
            if (array[i] is DBNull)
            {
                array[i] = null;
            }
        }
    }
}