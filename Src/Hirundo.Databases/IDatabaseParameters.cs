namespace Hirundo.Databases;

public interface IDatabaseParameters
{
    public string RingIdentifier { get; }
    public string DateIdentifier { get; }
    public string SpeciesIdentifier { get; }
    IList<ColumnParameters> Columns { get; }
}