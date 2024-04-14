using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population.WPF.IsNotMainSpecimen;

[ParametersData(
       typeof(IsNotMainSpecimenPopulationCondition),
       typeof(IsNotMainSpecimenModel),
       typeof(IsNotMainSpecimenView)
    )]
public class IsNotMainSpecimenViewModel(IsNotMainSpecimenModel model) : ParametersViewModel(model)
{
}
