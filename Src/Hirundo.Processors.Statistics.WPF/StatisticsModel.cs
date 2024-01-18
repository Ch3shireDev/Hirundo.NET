using Hirundo.Commons.WPF;
using Hirundo.Processors.Statistics.Operations;
using Hirundo.Processors.Statistics.WPF.Average;

namespace Hirundo.Processors.Statistics.WPF;

public class StatisticsModel : IParametersBrowserModel
{
    public StatisticsProcessorParameters StatisticsProcessorParameters { get; set; } = new();
    public string Header => "Statystyki";
    public string Title => "Operacje statystyczne";
    public string Description => "W tym panelu wybierasz dane statystyczne, które mają być obliczone dla populacji dla każdego osobnika powracającego.";
    public string AddParametersCommandText => "Dodaj operację";

    public IList<ParametersData> ParametersDataList { get; } =
    [
        new ParametersData(typeof(AverageOperation), "Średnia", "Średnia wartość ze wszystkich pomiarów")
    ];

    public void AddParameters(ParametersData parametersData)
    {
        AddOperation(parametersData.Type);
    }

    public IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return StatisticsProcessorParameters.Operations.Select(Create);
    }

    public void AddOperation(Type selectedType)
    {
        switch (selectedType.Name)
        {
            case nameof(AverageOperation):
                StatisticsProcessorParameters.Operations.Add(new AverageOperation());
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private ParametersViewModel Create(IStatisticalOperation operation)
    {
        ArgumentNullException.ThrowIfNull(operation, nameof(operation));
        var viewModel = (ParametersViewModel)(operation switch
        {
            AverageOperation operation1 => new AverageViewModel(new AverageModel(operation1)),
            _ => throw new ArgumentException($"Unknown operation model type: {operation.GetType()}")
        });

        if (viewModel is IRemovable removable)
        {
            removable.Removed += (_, args) =>
            {
                if (args.Parameters is IStatisticalOperation statisticalOperationToRemove)
                {
                    StatisticsProcessorParameters.Operations.Remove(statisticalOperationToRemove);
                }
            };
        }

        return viewModel;
    }
}