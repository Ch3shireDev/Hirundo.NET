using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.NotEarlierThanGivenDateNextYear;

[ParametersData(
    typeof(ReturnsNotEarlierThanGivenDateNextYearCondition),
    typeof(NotEarlierThanGivenDateNextYearModel),
    typeof(NotEarlierThanGivenDateNextYearView)
)]
public class NotEarlierThanGivenDateNextYearViewModel(NotEarlierThanGivenDateNextYearModel model) : ParametersViewModel(model)
{
    public IList<int> Months { get; } = Enumerable.Range(1, 12).ToList();

    public int Month
    {
        get => model.Month;
        set
        {
            model.Month = value;
            OnPropertyChanged();
        }
    }
    public int Day
    {
        get => model.Day;
        set
        {
            model.Day = value;
            OnPropertyChanged();
        }
    }
}