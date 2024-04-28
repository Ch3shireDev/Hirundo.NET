using Hirundo.Commons;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Population.Conditions;

[TypeDescription("IsNotMainSpecimen", "Czy osobnik nie jest wybranym osobnikiem powracającym?", "Dodaje warunek, by w populacji osobnika powracającego nie znajdował się sam osobnik.")]
public class IsNotMainSpecimenPopulationCondition : IPopulationCondition
{
    public IPopulationConditionClosure GetPopulationConditionClosure(Specimen returningSpecimen)
    {
        ArgumentNullException.ThrowIfNull(returningSpecimen, nameof(returningSpecimen));
        var ring = returningSpecimen.Ring;
        return new CustomClosure(specimen => specimen.Ring?.Equals(ring, StringComparison.OrdinalIgnoreCase) == false);
    }
}