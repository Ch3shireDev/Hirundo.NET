using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics.WPF;

public class StatisticsModel(ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository) : ParametersBrowserModel<StatisticsParameters, IStatisticalOperation, StatisticsModel>(labelsRepository, speciesRepository)
{
    public override string Header => "Statystyki";
    public override string Title => "Operacje statystyczne";
    public override string Description => "W tym panelu wybierasz dane statystyczne, które mają być obliczone dla populacji dla każdego osobnika powracającego.";
    public override string AddParametersCommandText => "Dodaj operację";
    public override IList<IStatisticalOperation> Parameters => ParametersContainer.Operations;
}