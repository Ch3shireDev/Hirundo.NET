﻿using Hirundo.Commons;

namespace Hirundo.Databases;

[TypeDescription("SqlServer")]
public class SqlServerParameters : IDatabaseParameters
{
    public string ConnectionString { get; set; } = null!;
    public string Table { get; set; } = null!;
    public IList<ColumnMapping> Columns { get; init; } = [];
}