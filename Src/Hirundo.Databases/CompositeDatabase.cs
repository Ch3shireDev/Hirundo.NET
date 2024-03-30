using Hirundo.Commons.Models;

namespace Hirundo.Databases;

/// <summary>
///     Złożona baza danych, która umożliwia pobieranie danych z wielu baz danych bądź tabel. Powstała w celu umożliwienia
///     pobierania danych z dwóch tabel o różnych strukturach w bazie danych Access.
/// </summary>
public class CompositeDatabase(params IDatabase[] databases) : IDatabase
{
    /// <summary>
    ///     Zwraca wszystkie wiersze z podanych baz danych.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Observation> GetObservations()
    {
        return databases.SelectMany(database => database.GetObservations());
    }
}