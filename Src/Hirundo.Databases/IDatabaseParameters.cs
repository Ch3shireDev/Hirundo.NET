namespace Hirundo.Databases;

public interface IDatabaseParameters
{
    IList<ColumnMapping> Columns { get; }
}