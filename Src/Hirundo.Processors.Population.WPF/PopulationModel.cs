using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;
using Hirundo.Processors.Population.WPF.IsInSharedTimeWindow;

namespace Hirundo.Processors.Population.WPF;

public class PopulationModel : IParametersBrowserModel
{
    public PopulationProcessorParameters ConfigPopulation { get; set; } = new();
    public IList<IPopulationConditionBuilder> Conditions => ConfigPopulation.Conditions;
    public string Header => "Populacja";
    public string Title => "Warunki populacji";
    public string Description => "W tym panelu określasz warunki określające populację dla danego osobnika powracającego.";
    public string AddParametersCommandText => "Dodaj nowy warunek";

    public IList<ParametersData> ParametersDataList { get; } =
    [
        new ParametersData(typeof(IsInSharedTimeWindowConditionBuilder), "Czy jest we współdzielonym oknie czasowym?", "Czy jest we współdzielonym oknie czasowym?")
    ];

    public IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return Conditions.Select(CreateViewModel);
    }

    public void AddParameters(ParametersData parametersData)
    {
        switch (parametersData.Type)
        {
            case not null when parametersData.Type == typeof(IsInSharedTimeWindowConditionBuilder):
                Conditions.Add(new IsInSharedTimeWindowConditionBuilder());
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public ParametersViewModel CreateViewModel(IPopulationConditionBuilder conditionBuilder)
    {
        var viewModel = Create(conditionBuilder);

        if (viewModel is IRemovable removable)
        {
            removable.Removed += (_, args) =>
            {
                if (args.Parameters is IPopulationConditionBuilder populationCondition)
                {
                    Conditions.Remove(populationCondition);
                }
            };
        }

        return viewModel;
    }

    public static ParametersViewModel Create(IPopulationConditionBuilder conditionBuilder)
    {
        return conditionBuilder switch
        {
            IsInSharedTimeWindowConditionBuilder m => new IsInSharedTimeWindowViewModel(new IsInSharedTimeWindowModel(m)),
            _ => throw new NotSupportedException($"Condition of type {conditionBuilder.GetType()} is not supported.")
        };
    }
}