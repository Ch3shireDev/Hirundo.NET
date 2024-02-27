
namespace Hirundo.Databases;

/// <summary>
///     Budowniczy obiektów typu <see cref="IDatabase" />. Jak na razie umożliwia dodawanie baz danych Access. Ze względu
///     na szczególną formę bieżącej bazy danych, budowniczy tworzy kompozyt, w celu umożliwienia pobierania danych z dwóch
///     tabel.
/// </summary>
public class DatabaseBuilder : IDatabaseBuilder
{
    private readonly List<IDatabase> _databases = [];

    public IDatabaseBuilder WithDatabaseParameters(IEnumerable<IDatabaseParameters> appConfigDatabases, CancellationToken? token = null)
    {
        ArgumentNullException.ThrowIfNull(appConfigDatabases);

        foreach (var databaseParameters in appConfigDatabases)
        {
            switch (databaseParameters)
            {
                case AccessDatabaseParameters accessDatabaseParameters:
                    AddMdbAccessDatabase(accessDatabaseParameters, token);
                    break;
                default:
                    throw new ArgumentException($"Unknown database type: {databaseParameters.GetType()}");
            }
        }

        return this;
    }

    /// <summary>
    ///     Dodaje parametry bazy danych Access.
    /// </summary>
    /// <param name="databaseParameters"></param>
    /// <returns></returns>
    public IDatabaseBuilder AddMdbAccessDatabase(AccessDatabaseParameters databaseParameters, CancellationToken? token = null)
    {
        _databases.Add(new MdbAccessDatabase(databaseParameters, token));
        return this;
    }

    /// <summary>
    ///     Tworzy obiekt typu <see cref="IDatabase" />.
    /// </summary>
    /// <returns></returns>
    public IDatabase Build()
    {
        return new CompositeDatabase([.. _databases]);
    }
}