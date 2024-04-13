using Hirundo.Commons.Models;

namespace Hirundo.Processors.Observations;

public class IsSpeciesEqualCondition : IObservationCondition
{
    public string Species { get; set; } = "";
    public bool IsAccepted(Observation observation)
    {
        return observation.Species?.Equals(Species, StringComparison.InvariantCultureIgnoreCase) == true;
    }
}
