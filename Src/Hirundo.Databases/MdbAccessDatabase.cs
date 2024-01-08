using System.Data.Odbc;
using Hirundo.Commons;

namespace Hirundo.Databases;

/// <summary>
///     Interfejs dla klas reprezentujących połączenie z bazą danych. Wszystkie klasy implementujące ten interfejs muszą
///     posiadać metodę GetData().
/// </summary>
public interface IDatabase
{
    public IEnumerable<Observation> GetObservations();
}

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

        var dataColumns = parameters.Columns.Select(x => x.DataValueName).ToArray();

        while (reader.Read())
        {
            var dataValues = GetValuesFromReader(reader);
            yield return new Observation(dataColumns, dataValues);
        }
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