namespace Hirundo.Databases;

/// <summary>
///     Budowniczy obiektów typu <see cref="IDatabase" />. Jak na razie umożliwia dodawanie baz danych Access. Ze względu
///     na szczególną formę bieżącej bazy danych, budowniczy tworzy kompozyt, w celu umożliwienia pobierania danych z dwóch
///     tabel.
/// </summary>
public class DatabaseBuilder
{
    private readonly IList<IDatabase> _databases = [];

    /// <summary>
    ///    Dodaje parametry bazy danych Access.
    /// </summary>
    /// <param name="databaseParameters"></param>
    /// <returns></returns>
    public DatabaseBuilder AddMdbAccessDatabase(AccessDatabaseParameters databaseParameters)
    {
        _databases.Add(new MdbAccessDatabase(databaseParameters));
        return this;
    }

    /// <summary>
    /// Tworzy obiekt typu <see cref="IDatabase" />.
    /// </summary>
    /// <returns></returns>
    public IDatabase Build()
    {
        return new CompositeDatabase(_databases.ToArray());
    }
}