
namespace Hirundo.Databases;

public interface IDatabaseBuilder
{
    IDatabaseBuilder WithDatabaseParameters(IEnumerable<IDatabaseParameters> appConfigDatabases, CancellationToken? token = null);

    /// <summary>
    ///     Dodaje parametry bazy danych Access.
    /// </summary>
    /// <param name="databaseParameters"></param>
    /// <returns></returns>
    IDatabaseBuilder AddMdbAccessDatabase(AccessDatabaseParameters databaseParameters, CancellationToken? token = null);

    /// <summary>
    ///     Tworzy obiekt typu <see cref="IDatabase" />.
    /// </summary>
    /// <returns></returns>
    IDatabase Build();

    /// <summary>
    ///    Ustala token anulowania operacji.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
}