using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.WPF.Population.IsPopulationInDateRange;

[ParametersData(
    typeof(IsPopulationInDateRangeCondition),
    typeof(IsPopulationInDateRangeModel),
    typeof(IsPopulationInDateRangeView)
)]
public class IsPopulationInDateRangeViewModel(IsPopulationInDateRangeModel model) : ParametersViewModel(model)
{
    public int MonthFrom
    {
        get => model.MonthFrom;
        set
        {
            model.MonthFrom = value;
            OnPropertyChanged();
        }
    }
    
    public int MonthTo
    {
        get => model.MonthTo;
        set
        {
            model.MonthTo = value;
            OnPropertyChanged();
        }
    }
    
    public int DayFrom
    {
        get => model.DayFrom;
        set
        {
            model.DayFrom = value;
            OnPropertyChanged();
        }
    }
    
    public int DayTo
    {
        get => model.DayTo;
        set
        {
            model.DayTo = value;
            OnPropertyChanged();
        }
    }
}