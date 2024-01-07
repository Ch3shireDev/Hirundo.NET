using Hirundo.Commons;

namespace Hirundo.Filters.Population;

public interface IPopulationFilter
{
    bool IsAccepted(Observation observation);
}