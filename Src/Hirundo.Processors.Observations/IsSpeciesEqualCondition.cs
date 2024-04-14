using Hirundo.Commons;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Observations;

[TypeDescription(
    "IsSpeciesEqual",
    "Czy należy do gatunku?",
    "Warunek sprawdzający, czy zaobserwowany osobnik należy do wskazanego gatunku."
    )]
public class IsSpeciesEqualCondition : IObservationCondition
{
    public string Species { get; set; } = "";
    public bool IsAccepted(Observation observation)
    {
        return observation.Species?.Equals(Species, StringComparison.InvariantCultureIgnoreCase) == true;
    }
}
