using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;
using Hirundo.Processors.Returning.WPF.AfterTimePeriod;
using Hirundo.Processors.Returning.WPF.NotEarlierThanGivenDateNextYear;

namespace Hirundo.Processors.Returning.WPF;

public class ReturningSpecimensModel : IParametersBrowserModel
{
    public ReturningSpecimensParameters? ReturningSpecimensParameters { get; set; } = new();
    public IList<IReturningSpecimenCondition> Conditions => ReturningSpecimensParameters!.Conditions;

    public string Header => "Powroty";
    public string Title => "Warunki powracających osobników";
    public string Description => "W tym panelu ustalasz warunki wyróżniające osobniki powracające spośród wszystkich osobników.";
    public string AddParametersCommandText => "Dodaj warunek";

    public IList<ParametersData> ParametersDataList { get; } =
    [
        new ParametersData(
            typeof(ReturnsAfterTimePeriodCondition), "Powrót po określonym czasie", "Osobnik wraca nie wcześniej niż po określonej liczbie dni"),
        new ParametersData(typeof(ReturnsNotEarlierThanGivenDateNextYearCondition), "Powrót po określonej dacie w przyszłym roku", "Osobnik wraca nie wcześniej niż w określonej dacie w przyszłym roku")
    ];

    public void AddParameters(ParametersData parametersData)
    {
        AddParameters(parametersData.Type);
    }

    public IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return Conditions.Select(Create);
    }

    private void AddParameters(Type parametersDataType)
    {
        if (parametersDataType == typeof(ReturnsAfterTimePeriodCondition))
        {
            Conditions.Add(new ReturnsAfterTimePeriodCondition());
        }
        else if (parametersDataType == typeof(ReturnsNotEarlierThanGivenDateNextYearCondition))
        {
            Conditions.Add(new ReturnsNotEarlierThanGivenDateNextYearCondition());
        }
        else
        {
            throw new ArgumentException($"Unknown type: {parametersDataType}", nameof(parametersDataType));
        }
    }

    private ParametersViewModel Create(IReturningSpecimenCondition condition)
    {
        var viewModel = (ParametersViewModel)(condition switch
        {
            ReturnsNotEarlierThanGivenDateNextYearCondition m => new NotEarlierThanGivenDateNextYearViewModel(new NotEarlierThanGivenDateNextYearModel(m)),
            ReturnsAfterTimePeriodCondition m => new AfterTimePeriodViewModel(new AfterTimePeriodModel(m)),
            _ => throw new NotImplementedException()
        });

        if (viewModel is IRemovable removable)
        {
            removable.Removed += (_, args) =>
            {
                if (args.Parameters is IReturningSpecimenCondition filter)
                {
                    Conditions.Remove(filter);
                }
            };
        }

        return viewModel;
    }
}