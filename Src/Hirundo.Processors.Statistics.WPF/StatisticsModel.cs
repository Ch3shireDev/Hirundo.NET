using Hirundo.Commons.WPF;
using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics.WPF;

public class StatisticsModel : ParametersBrowserModel
{
    private readonly IStatisticsParametersFactory _factory;

    public StatisticsModel(IStatisticsParametersFactory factory)
    {
        _factory = factory;
        ParametersDataList = _factory.GetParametersData().ToArray();
    }

    public StatisticsProcessorParameters StatisticsProcessorParameters { get; set; } = new();
    public override string Header => "Statystyki";
    public override string Title => "Operacje statystyczne";
    public override string Description => "W tym panelu wybierasz dane statystyczne, które mają być obliczone dla populacji dla każdego osobnika powracającego.";
    public override string AddParametersCommandText => "Dodaj operację";

    public override IList<ParametersData> ParametersDataList { get; }

    public override void AddParameters(ParametersData parametersData)
    {
        var condition = _factory.CreateCondition(parametersData);
        StatisticsProcessorParameters.Operations.Add(condition);
    }

    public override IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return StatisticsProcessorParameters
                .Operations
                .Select(_factory.CreateViewModel)
                .Select(AddEventListener)
            ;
    }


    private ParametersViewModel AddEventListener(ParametersViewModel viewModel)
    {
        if (viewModel is not IRemovable removable) return viewModel;

        removable.Removed += (_, args) =>
        {
            if (args.Parameters is IStatisticalOperation statisticalOperationToRemove)
            {
                StatisticsProcessorParameters.Operations.Remove(statisticalOperationToRemove);
            }
        };

        return viewModel;
    }
}