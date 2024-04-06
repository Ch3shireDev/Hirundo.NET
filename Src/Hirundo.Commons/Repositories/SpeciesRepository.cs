namespace Hirundo.Commons.Repositories;

public interface ISpeciesRepository
{
    event EventHandler? LabelsChanged;
    IList<string> GetSpecies();
}

public class SpeciesRepository : ISpeciesRepository
{
    public event EventHandler? LabelsChanged;
    public IList<string> GetSpecies()
    {
        return new List<string>();
    }
}
