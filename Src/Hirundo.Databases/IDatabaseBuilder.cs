namespace Hirundo.Databases;

public interface IDatabaseBuilder
{
    IDatabaseBuilder WithDatabaseParameters(params IDatabaseParameters[] appConfigDatabases);
    IDatabaseBuilder WithDatabaseParameters(IEnumerable<IDatabaseParameters> appConfigDatabases);

    /// <summary>
    ///     Dodaje parametry bazy danych Access.
    /// </summary>
    /// <param name="databaseParameters"></param>
    /// <returns></returns>
    IDatabaseBuilder AddMdbAccessDatabase(AccessDatabaseParameters databaseParameters);

    /// <summary>
    ///     Tworzy obiekt typu <see cref="IDatabase" />.
    /// </summary>
    /// <returns></returns>
    IDatabase Build();
}