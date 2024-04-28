namespace Hirundo.Commons.Models;

public class ReturningSpecimen(Specimen specimen, IEnumerable<Specimen> population)
{
    public string Ring => Specimen.Ring;
    public Specimen Specimen { get; } = specimen;
    public IEnumerable<Specimen> Population { get; } = population;
}