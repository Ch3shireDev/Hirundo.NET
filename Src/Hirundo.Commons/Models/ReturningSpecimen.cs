namespace Hirundo.Commons.Models;
public class ReturningSpecimen
{
    public string Ring => Specimen.Ring;
    public Specimen Specimen { get; }
    public IEnumerable<Specimen> Population { get; }

    public ReturningSpecimen(Specimen specimen, IEnumerable<Specimen> population)
    {
        Specimen = specimen;
        Population = population;
    }

}
