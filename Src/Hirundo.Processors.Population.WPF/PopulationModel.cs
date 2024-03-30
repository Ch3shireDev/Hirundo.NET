using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population.WPF;

public class PopulationModel(IDataLabelRepository repository) : ParametersBrowserModel<PopulationParameters, IPopulationConditionBuilder, PopulationModel>(repository)
{
    public override string Header => "Populacja";
    public override string Title => "Warunki populacji";
    public override string Description => "W tym panelu określasz warunki określające populację dla danego osobnika powracającego.";
    public override string AddParametersCommandText => "Dodaj nowy warunek";
    public override IList<IPopulationConditionBuilder> Parameters => ParametersContainer.Conditions;
}