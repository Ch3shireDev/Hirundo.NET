using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population.WPF;

public class PopulationModel : ParametersBrowserModel<PopulationProcessorParameters, IPopulationConditionBuilder>
{
    public IList<IPopulationConditionBuilder> Conditions => ParametersContainer.Conditions;
    public override string Header => "Populacja";
    public override string Title => "Warunki populacji";
    public override string Description => "W tym panelu określasz warunki określające populację dla danego osobnika powracającego.";
    public override string AddParametersCommandText => "Dodaj nowy warunek";

    public override IList<ParametersData> ParametersDataList { get; } =
    [
        new ParametersData(typeof(IsInSharedTimeWindowConditionBuilder), "Czy jest we współdzielonym oknie czasowym?", "Czy jest we współdzielonym oknie czasowym?")
    ];

    public override IList<IPopulationConditionBuilder> Parameters => ParametersContainer.Conditions;

    public PopulationModel(IPopulationParametersFactory factory)
    {
        _factory = factory;
        ParametersDataList = _factory.GetParametersData().ToArray();
    }

    private readonly IPopulationParametersFactory _factory;

    public override IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return
            ParametersContainer
                .Conditions
                .Select(_factory.CreateViewModel)
                .Select(AddEventListener)
            ;
    }

    public override void AddParameters(ParametersData parametersData)
    {
        var condition = _factory.CreateCondition(parametersData);
        ParametersContainer.Conditions.Add(condition);
    }
}