using Hirundo.Commons;

namespace Hirundo.Filters.Population;

public class IsInSharedTimeWindowFilter(
    Specimen returningSpecimen,
    string dateValueName,
    int days) : IPopulationFilter
{
    public bool IsAccepted(Observation observation)
    {
        return true;
    }
}