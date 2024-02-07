using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations.Conditions;
using Hirundo.Processors.Observations.WPF.IsEqual;
using Hirundo.Processors.Observations.WPF.IsInSet;
using Hirundo.Processors.Observations.WPF.IsInTimeBlock;


namespace Hirundo.Processors.Observations.WPF;

public class ObservationParametersBrowserModel(IDataLabelRepository repository) : ParametersBrowserModel
{
    public ObservationsParameters ObservationsParameters { get; set; } = new();
    public override string Description => "W tym panelu ustalasz warunki, jakie mają spełniać wybierane obserwacje do obliczeń.";
    public override string AddParametersCommandText => "Dodaj nowy warunek";
    public override string Header => "Obserwacje";
    public override string Title => "Warunki filtrowania obserwacji";

    public override IList<ParametersData> ParametersDataList { get; } =
    [
        new ParametersData(typeof(IsEqualCondition), "Czy wartość jest równa?", "Warunek porównujący wartość w polu danych z wybraną wartością."),
        new ParametersData(typeof(IsInTimeBlockCondition), "Czy wartość jest w przedziale czasowym?", "Warunek sprawdzający godziny złapania osobnika."),
        new ParametersData(typeof(IsInSetCondition), "Czy wartość jest w zbiorze?", "Warunek sprawdzający, czy wartość obserwacji z zadanego pola znajduje się w zbiorze wartości.")
    ];

    public override void AddParameters(ParametersData parametersData)
    {
        switch (parametersData.Type.Name)
        {
            case nameof(IsEqualCondition):
                ObservationsParameters.Conditions.Add(new IsEqualCondition());
                break;
            case nameof(IsInTimeBlockCondition):
                ObservationsParameters.Conditions.Add(new IsInTimeBlockCondition());
                break;
            case nameof(IsInSetCondition):
                ObservationsParameters.Conditions.Add(new IsInSetCondition());
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public override IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return
            ObservationsParameters
                .Conditions
                .Select(Create);
    }

    private ParametersViewModel Create(IObservationCondition condition)
    {
        var viewModel = (ParametersViewModel)(condition switch
        {
            IsEqualCondition filter => new IsEqualViewModel(new IsEqualModel(filter, repository)),
            IsInTimeBlockCondition filter => new IsInTimeBlockViewModel(new IsInTimeBlockModel(filter, repository)),
            IsInSetCondition filter => new IsInSetViewModel(new IsInSetModel(filter, repository)),
            _ => throw new NotImplementedException()
        });

        if (viewModel is IRemovable removable)
        {
            removable.Removed += (_, args) =>
            {
                if (args.Parameters is IObservationCondition conditionToRemove)
                {
                    ObservationsParameters.Conditions.Remove(conditionToRemove);
                }
            };
        }

        return viewModel;
    }
}