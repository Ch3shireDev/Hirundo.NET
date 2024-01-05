using Hirundo.Commons;

namespace Hirundo.Databases;

/// <summary>
///     Złożona baza danych, która umożliwia pobieranie danych z wielu baz danych bądź tabel. Powstała w celu umożliwienia
///     pobierania danych z dwóch tabel o różnych strukturach w bazie danych Access.
/// </summary>
public class CompositeDatabase : IDatabase
{
    private readonly IDatabase[] _databases;

    /// <summary>
    ///     Konstruktor przyjmujący tablicę obiektów typu <see cref="IDatabase" />.
    /// </summary>
    /// <param name="databases"></param>
    public CompositeDatabase(params IDatabase[] databases)
    {
        _databases = databases;
    }

    /// <summary>
    ///     Zwraca wszystkie wiersze z podanych baz danych.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Observation> GetObservations()
    {
        return _databases.SelectMany(database => database.GetObservations());
    }
}