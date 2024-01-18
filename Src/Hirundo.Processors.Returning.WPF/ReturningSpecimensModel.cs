using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;
using Hirundo.Processors.Returning.WPF.AfterTimePeriod;
using Hirundo.Processors.Returning.WPF.NotEarlierThanGivenDateNextYear;

namespace Hirundo.Processors.Returning.WPF;

public class ReturningSpecimensModel : IParametersBrowserModel
{
    public ReturningSpecimensParameters? ReturningSpecimensParameters { get; set; } = new();
    public IList<IReturningSpecimenFilter> Conditions => ReturningSpecimensParameters!.Conditions;

    public string Header => "Powroty";
    public string Title => "Warunki powracających osobników";
    public string Description => "W tym panelu ustalasz warunki wyróżniające osobniki powracające spośród wszystkich osobników.";
    public string AddParametersCommandText => "Dodaj warunek";

    public IList<ParametersData> ParametersDataList { get; } =
    [
        new ParametersData(
            typeof(ReturnsAfterTimePeriodFilter), "Powrót po określonym czasie", "Osobnik wraca nie wcześniej niż po określonej liczbie dni"),
        new ParametersData(typeof(ReturnsNotEarlierThanGivenDateNextYearFilter), "Powrót po określonej dacie w przyszłym roku", "Osobnik wraca nie wcześniej niż w określonej dacie w przyszłym roku")
    ];

    public void AddParameters(ParametersData parametersData)
    {
        if (parametersData.Type == typeof(ReturnsAfterTimePeriodFilter))
        {
            Conditions.Add(new ReturnsAfterTimePeriodFilter());
        }
        else if (parametersData.Type == typeof(ReturnsNotEarlierThanGivenDateNextYearFilter))
        {
            Conditions.Add(new ReturnsNotEarlierThanGivenDateNextYearFilter());
        }
        else
        {
            throw new ArgumentException($"Unknown type: {parametersData.Type}", nameof(parametersData.Type));
        }
    }

    public IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return Conditions.Select(Create);
    }

    private ParametersViewModel Create(IReturningSpecimenFilter condition)
    {
        var viewModel = (ParametersViewModel)(condition switch
        {
            ReturnsNotEarlierThanGivenDateNextYearFilter m => new NotEarlierThanGivenDateNextYearViewModel(new NotEarlierThanGivenDateNextYearModel(m)),
            ReturnsAfterTimePeriodFilter m => new AfterTimePeriodViewModel(new AfterTimePeriodModel(m)),
            _ => throw new NotImplementedException()
        });

        if (viewModel is IRemovable removable)
        {
            removable.Removed += (_, args) =>
            {
                if (args.Parameters is IReturningSpecimenFilter filter)
                {
                    Conditions.Remove(filter);
                }
            };
        }

        return viewModel;
    }
}