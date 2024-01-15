namespace Hirundo.Processors.Returning.WPF.AfterTimePeriod;

internal class AfterTimePeriodViewModel(AfterTimePeriodModel model) : ReturningSpecimensConditionViewModel
{
    public string DateValueName
    {
        get => model.DateValueName;
        set
        {
            model.DateValueName = value;
            OnPropertyChanged();
        }
    }

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