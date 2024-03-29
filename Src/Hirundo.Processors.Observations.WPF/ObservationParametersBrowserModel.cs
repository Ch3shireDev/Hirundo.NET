using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF;

public class ObservationParametersBrowserModel : ParametersBrowserModel<ObservationsParameters, IObservationCondition>
{
    public ObservationParametersBrowserModel(IObservationParametersFactory factory) : base(factory) { }

    public override string Description => "W tym panelu ustalasz warunki, jakie mają spełniać wybierane obserwacje do obliczeń.";
    public override string AddParametersCommandText => "Dodaj nowy warunek";
    public override string Header => "Obserwacje";
    public override string Title => "Warunki obserwacji";

    public override IList<IObservationCondition> Parameters => ParametersContainer.Conditions;

    public override void AddParameters(ParametersData parametersData)
    {
        var condition = _factory.CreateCondition(parametersData);
        ParametersContainer.Conditions.Add(condition);
    }

    public override IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return
            ParametersContainer
                .Conditions
                .Select(_factory.CreateViewModel)
                .Select(AddEventListener)
            ;
    }
}