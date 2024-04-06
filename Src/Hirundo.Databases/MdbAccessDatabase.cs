using Hirundo.Commons.Models;
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
    public CancellationToken? CancellationToken => token;
    public AccessDatabaseParameters? Parameters => parameters;

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
        ThrowIfRingIdentifierIsInvalid();
        ThrowIfDateIdentifierIsInvalid();

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


        Log.Information($"Odczytywanie danych z tabeli {parameters.Table}.");

        var headers = GetDataColumns(parameters);
        int ringIndex = GetRingIndex(parameters);
        int dateIndex = GetDateIndex(parameters);

        while (reader.Read())
        {
            var values = GetValuesFromReader(reader);

            var ring = values[ringIndex] as string ?? "";
            var date = values[dateIndex] as DateTime? ?? DateTime.MinValue;

            yield return new Observation
            {
                Ring = ring,
                Date = date,
                Headers = headers,
                Values = values
            };

            token?.ThrowIfCancellationRequested();
        }

        Log.Information("Zakończono odczyt danych z bazy danych Access.");
    }

    private void ThrowIfRingIdentifierIsInvalid()
    {
        if (GetRingIndex(parameters) == -1)
        {
            throw new ArgumentException("Należy podać nazwę pola danych z numerem obrączki.");
        }
    }

    private void ThrowIfDateIdentifierIsInvalid()
    {
        if (GetDateIndex(parameters) == -1)
        {
            throw new ArgumentException("Należy podać nazwę pola danych z datą obserwacji.");
        }
    }
    private static int GetDateIndex(AccessDatabaseParameters parameters)
    {
        var dataColumns2 = GetDataColumns(parameters);
        var dateIndex = Array.IndexOf(dataColumns2, parameters.DateIdentifier);
        return dateIndex;
    }

    private static int GetRingIndex(AccessDatabaseParameters parameters)
    {
        var dataColumns = GetDataColumns(parameters);
        var ringIndex = Array.IndexOf(dataColumns, parameters.RingIdentifier);
        return ringIndex;
    }

    private static string[] GetDataColumns(AccessDatabaseParameters parameters)
    {
        return parameters.Columns.Select(x => x.ValueName).ToArray();
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