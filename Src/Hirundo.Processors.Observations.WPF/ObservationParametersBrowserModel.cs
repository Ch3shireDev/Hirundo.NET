using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF;

public class ObservationParametersBrowserModel(IObservationParametersFactory factory) : ParametersBrowserModel
{
    public ObservationsParameters ObservationsParameters { get; set; } = new();
    public override string Description => "W tym panelu ustalasz warunki, jakie mają spełniać wybierane obserwacje do obliczeń.";
    public override string AddParametersCommandText => "Dodaj nowy warunek";
    public override string Header => "Obserwacje";
    public override string Title => "Warunki obserwacji";

    public override IList<ParametersData> ParametersDataList => factory.GetParametersData().ToArray();

    public override void AddParameters(ParametersData parametersData)
    {
        var condition = factory.CreateCondition(parametersData);
        ObservationsParameters.Conditions.Add(condition);
    }

    public override IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return
            ObservationsParameters
                .Conditions
                .Select(factory.CreateViewModel)
                .Select(AddEventListener)
            ;
    }

    private ParametersViewModel AddEventListener(ParametersViewModel viewModel)
    {
        if (viewModel is not IRemovable removable) return viewModel;

        removable.Removed += (_, args) =>
        {
            if (args.Parameters is IObservationCondition conditionToRemove)
            {
                ObservationsParameters.Conditions.Remove(conditionToRemove);
            }
        };

        return viewModel;
    }
}