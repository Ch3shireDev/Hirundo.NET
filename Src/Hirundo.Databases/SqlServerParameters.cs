﻿using Hirundo.Commons;

namespace Hirundo.Databases;

[TypeDescription("SqlServer", "SQL Server", "Pobiera dane z serwera SQL Server.")]
public class SqlServerParameters : IDatabaseParameters
{
    public string ConnectionString { get; set; } = null!;
    public string Table { get; set; } = null!;
    public string RingIdentifier { get; set; } = null!;
    public string DateIdentifier { get; set; } = null!;
    public string SpeciesIdentifier { get; set; } = null!;
    public IList<ColumnParameters> Columns { get; init; } = [];
}