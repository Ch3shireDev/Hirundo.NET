﻿using Serilog;

namespace Hirundo.Databases;

/// <summary>
///     Budowniczy obiektów typu <see cref="IDatabase" />. Jak na razie umożliwia dodawanie baz danych Access. Ze względu
///     na szczególną formę bieżącej bazy danych, budowniczy tworzy kompozyt, w celu umożliwienia pobierania danych z dwóch
///     tabel.
/// </summary>
public class DatabaseBuilder : IDatabaseBuilder
{
    private readonly List<Func<IDatabase>> _builders = [];

    private CancellationToken? _token;

    /// <summary>
    ///     Tworzy obiekt typu <see cref="IDatabase" />.
    /// </summary>
    /// <returns></returns>
    public IDatabase Build()
    {
        Log.Information("Budowanie źródeł baz danych. Liczba baz danych: {_buildersCount}.", _builders.Count);

        if (_builders.Count == 1)
        {
            return _builders[0]();
        }

        return new CompositeDatabase([.. _builders.Select(x => x())]);
    }

    public IDatabaseBuilder NewBuilder()
    {
        return new DatabaseBuilder();
    }

    public IDatabaseBuilder WithDatabaseParameters(IList<IDatabaseParameters> appConfigDatabases)
    {
        ArgumentNullException.ThrowIfNull(appConfigDatabases, nameof(appConfigDatabases));

        foreach (var databaseParameters in appConfigDatabases)
        {
            switch (databaseParameters)
            {
                case AccessDatabaseParameters accessDatabaseParameters:
                    _builders.Add(() => new MdbAccessDatabase(accessDatabaseParameters, _token));
                    break;
                case ExcelDatabaseParameters xlsxDatabaseParameters:
                    _builders.Add(() => new XlsxDatabase(xlsxDatabaseParameters, _token));
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
    public IDatabaseBuilder AddMdbAccessDatabase(AccessDatabaseParameters databaseParameters)
    {
        _builders.Add(() => new MdbAccessDatabase(databaseParameters, _token));

        return this;
    }

    /// <summary>
    ///     Dodaje token anulowania operacji.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public IDatabaseBuilder WithCancellationToken(CancellationToken? cancellationToken)
    {
        _token = cancellationToken;
        return this;
    }
}