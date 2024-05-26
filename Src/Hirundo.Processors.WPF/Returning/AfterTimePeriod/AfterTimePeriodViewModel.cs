using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.WPF.Returning.AfterTimePeriod;

[ParametersData(
    typeof(ReturnsAfterTimePeriodCondition),
    typeof(AfterTimePeriodModel),
    typeof(AfterTimePeriodView)
)]
public class AfterTimePeriodViewModel(AfterTimePeriodModel model) : ParametersViewModel(model), IRemovable
{
    public int TimePeriodInDays
    {
        get => model.TimePeriodInDays;
        set
        {
            model.TimePeriodInDays = value;
            OnPropertyChanged();
        }
    }
}