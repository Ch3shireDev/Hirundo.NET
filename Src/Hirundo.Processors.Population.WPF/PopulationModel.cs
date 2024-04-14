using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population.WPF;

public class PopulationModel(ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository) : ParametersBrowserModel<PopulationParameters, IPopulationCondition, PopulationModel>(labelsRepository, speciesRepository)
{
    public override string Header => "Populacja";
    public override string Title => "Warunki populacji";
    public override string Description => "W tym panelu określasz warunki określające populację dla danego osobnika powracającego.";
    public override string AddParametersCommandText => "Dodaj nowy warunek";
    public override IList<IPopulationCondition> Parameters => ParametersContainer.Conditions;
}