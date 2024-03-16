
namespace Hirundo.Databases;

public interface IDatabaseBuilder
{
    IDatabaseBuilder WithDatabaseParameters(params IDatabaseParameters[] appConfigDatabases);

    /// <summary>
    ///     Dodaje parametry bazy danych Access.
    /// </summary>
    /// <param name="databaseParameters"></param>
    /// <returns></returns>
    IDatabaseBuilder AddMdbAccessDatabase(AccessDatabaseParameters databaseParameters);

    /// <summary>
    ///     Dodaje token anulowania operacji.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    IDatabaseBuilder WithCancellationToken(CancellationToken? cancellationToken);

    /// <summary>
    ///     Tworzy nowego Budowniczego tego samego typu.
    /// </summary>
    /// <returns></returns>
    IDatabaseBuilder NewBuilder();

    /// <summary>
    ///     Tworzy obiekt typu <see cref="IDatabase" />.
    /// </summary>
    /// <returns></returns>
    IDatabase Build();
}