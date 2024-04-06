namespace Hirundo.Databases;

public interface IDatabaseParameters
{
    public string RingIdentifier { get; }
    public string DateIdentifier { get; }

    IList<ColumnParameters> Columns { get; }
}