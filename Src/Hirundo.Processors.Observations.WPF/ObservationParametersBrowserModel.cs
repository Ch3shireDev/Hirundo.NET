using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations.Conditions;
using Hirundo.Processors.Observations.WPF.IsEqual;
using Hirundo.Processors.Observations.WPF.IsInTimeBlock;

namespace Hirundo.Processors.Observations.WPF;

public class ObservationParametersBrowserModel : IParametersBrowserModel
{
    public ObservationsParameters ObservationsParameters { get; set; } = new();
    public string Description => "W tym panelu ustalasz warunki, jakie mają spełniać wybierane obserwacje do obliczeń.";
    public string AddParametersCommandText => "Dodaj nowy warunek";
    public string Header => "Obserwacje";
    public string Title => "Warunki filtrowania obserwacji";

    public IList<ParametersData> ParametersDataList { get; } =
    [
        new ParametersData(typeof(IsEqualFilter), "Czy wartość jest równa?", "Warunek porównujący wartość w polu danych z wybraną wartością."),
        new ParametersData(typeof(IsInTimeBlockFilter), "Czy wartość jest w przedziale czasowym?", "Warunek sprawdzający godziny złapania osobnika.")
    ];

    public void AddParameters(ParametersData parametersData)
    {
        switch (parametersData.Type.Name)
        {
            case nameof(IsEqualFilter):
                ObservationsParameters.Conditions.Add(new IsEqualFilter());
                break;
            case nameof(IsInTimeBlockFilter):
                ObservationsParameters.Conditions.Add(new IsInTimeBlockFilter());
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return
            ObservationsParameters
                .Conditions
                .Select(Create);
    }

    private ParametersViewModel Create(IObservationFilter condition)
    {
        var viewModel = (ParametersViewModel)(condition switch
        {
            IsEqualFilter filter => new IsEqualViewModel(new IsEqualModel(filter)),
            IsInTimeBlockFilter filter => new IsInTimeBlockViewModel(new IsInTimeBlockModel(filter)),
            _ => throw new NotImplementedException()
        });

        if (viewModel is IRemovable removable)
        {
            removable.Removed += (_, args) =>
            {
                if (args.Condition is IObservationFilter conditionToRemove)
                {
                    ObservationsParameters.Conditions.Remove(conditionToRemove);
                }
            };
        }

        return viewModel;
    }
}