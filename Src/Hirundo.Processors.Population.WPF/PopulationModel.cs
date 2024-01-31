using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;
using Hirundo.Processors.Population.WPF.IsInSharedTimeWindow;

namespace Hirundo.Processors.Population.WPF;

public class PopulationModel(IDataLabelRepository repository) : ParametersBrowserModel
{
    public PopulationProcessorParameters ConfigPopulation { get; set; } = new();
    public IList<IPopulationConditionBuilder> Conditions => ConfigPopulation.Conditions;
    public override string Header => "Populacja";
    public override string Title => "Warunki populacji";
    public override string Description => "W tym panelu określasz warunki określające populację dla danego osobnika powracającego.";
    public override string AddParametersCommandText => "Dodaj nowy warunek";

    public override IList<ParametersData> ParametersDataList { get; } =
    [
        new ParametersData(typeof(IsInSharedTimeWindowConditionBuilder), "Czy jest we współdzielonym oknie czasowym?", "Czy jest we współdzielonym oknie czasowym?")
    ];

    public override IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return Conditions.Select(CreateViewModel);
    }

    public override void AddParameters(ParametersData parametersData)
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

    public ParametersViewModel Create(IPopulationConditionBuilder conditionBuilder)
    {
        return conditionBuilder switch
        {
            IsInSharedTimeWindowConditionBuilder m => new IsInSharedTimeWindowViewModel(new IsInSharedTimeWindowModel(m, repository)),
            _ => throw new NotSupportedException($"Condition of type {conditionBuilder.GetType()} is not supported.")
        };
    }
}