using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;
using Hirundo.Processors.Population.WPF.IsInSharedTimeWindow;

namespace Hirundo.Processors.Population.WPF;

public class PopulationModel : IParametersBrowserModel
{
    public PopulationProcessorParameters ConfigPopulation { get; set; } = new();
    public IList<IPopulationFilterBuilder> Conditions => ConfigPopulation.Conditions;
    public string Header => "Populacja";
    public string Title => "Warunki populacji";
    public string Description => "W tym panelu określasz warunki określające populację dla danego osobnika powracającego.";
    public string AddParametersCommandText => "Dodaj nowy warunek";

    public IList<ParametersData> ParametersDataList { get; } =
    [
        new ParametersData(typeof(IsInSharedTimeWindowFilterBuilder), "Czy jest we współdzielonym oknie czasowym?", "Czy jest we współdzielonym oknie czasowym?")
    ];

    public IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return Conditions.Select(CreateViewModel);
    }

    public void AddParameters(ParametersData parametersData)
    {
        switch (parametersData.Type)
        {
            case not null when parametersData.Type == typeof(IsInSharedTimeWindowFilterBuilder):
                Conditions.Add(new IsInSharedTimeWindowFilterBuilder());
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public ParametersViewModel CreateViewModel(IPopulationFilterBuilder condition)
    {
        var viewModel = Create(condition);

        if (viewModel is IRemovable removable)
        {
            removable.Removed += (_, args) =>
            {
                if (args.Parameters is IPopulationFilterBuilder populationCondition)
                {
                    Conditions.Remove(populationCondition);
                }
            };
        }

        return viewModel;
    }

    public static ParametersViewModel Create(IPopulationFilterBuilder condition)
    {
        return condition switch
        {
            IsInSharedTimeWindowFilterBuilder m => new IsInSharedTimeWindowViewModel(new IsInSharedTimeWindowModel(m)),
            _ => throw new NotSupportedException($"Condition of type {condition.GetType()} is not supported.")
        };
    }
}