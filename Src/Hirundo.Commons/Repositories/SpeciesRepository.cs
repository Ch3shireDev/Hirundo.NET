namespace Hirundo.Commons.Repositories;

public interface ISpeciesRepository
{
    event EventHandler? SpeciesChanged;
    IList<string> GetSpecies();
    void UpdateSpecies(IList<string> speciesList);
}

public class SpeciesRepository : ISpeciesRepository
{
    private readonly HashSet<string> _species = [];

    public event EventHandler? SpeciesChanged;
    public IList<string> GetSpecies()
    {
        return [.. _species];
    }

    public void UpdateSpecies(IList<string> speciesList)
    {
        ArgumentNullException.ThrowIfNull(speciesList);

        foreach (var species in speciesList)
        {
            _species.Add(species);
        }

        SpeciesChanged?.Invoke(this, EventArgs.Empty);
    }
}
