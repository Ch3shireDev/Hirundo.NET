namespace Hirundo.Databases;

public interface IDatabaseParameters
{
    IList<ColumnParameters> Columns { get; }
}