using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF;

public class ReturningSpecimensModel(ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : ParametersBrowserModel<ReturningParameters, IReturningSpecimenCondition, ReturningSpecimensModel>(labelsRepository, speciesRepository)
{
    public override string Header => "Powroty";
    public override string Title => "Warunki powracających osobników";
    public override string Description => "W tym panelu ustalasz warunki wyróżniające osobniki powracające spośród wszystkich osobników.";
    public override string AddParametersCommandText => "Dodaj warunek";
    public override IList<IReturningSpecimenCondition> Parameters => ParametersContainer.Conditions;
}