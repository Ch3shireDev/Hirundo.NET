using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Observations;

[TypeDescription(
    "IsSpeciesEqual",
    "Czy należy do gatunku?",
    "Warunek sprawdzający, czy zaobserwowany osobnik należy do wskazanego gatunku."
)]
public class IsSpeciesEqualCondition : IObservationCondition, ISelfExplainer
{
    public string Species { get; set; } = "";

    public bool IsAccepted(Observation observation)
    {
        return observation.Species?.Equals(Species, StringComparison.InvariantCultureIgnoreCase) == true;
    }

    public string Explain()
    {
        return $"Osobniki muszą mieć identyfikator gatunku '{Species}'.";
    }
}