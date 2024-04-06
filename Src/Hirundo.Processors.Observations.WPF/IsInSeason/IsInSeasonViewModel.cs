using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.IsInSeason;

[ParametersData(
    typeof(IsInSeasonCondition),
    typeof(IsInSeasonModel),
    typeof(IsInSeasonView),
    "Czy dane są w sezonie?",
    "Sprawdza, czy dana obserwacja zaszła w zadanym przedziale dat, dowolnego roku.")
]
public class IsInSeasonViewModel(IsInSeasonModel model) : ParametersViewModel(model)
{
    public IList<int> Months { get; } = Enumerable.Range(1, 12).ToList();

    public int StartMonth
    {
        get => model.StartMonth;
        set
        {
            model.StartMonth = value;
            OnPropertyChanged();
        }
    }

    public int StartDay
    {
        get => model.StartDay;
        set
        {
            model.StartDay = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }

    public int EndMonth
    {
        get => model.EndMonth;
        set
        {
            model.EndMonth = value;
            OnPropertyChanged();
        }
    }

    public int EndDay
    {
        get => model.EndDay;
        set
        {
            model.EndDay = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }

    public string ErrorMessage => GetErrorMessage();

    private string GetErrorMessage()
    {
        if (StartDay > 31 || StartDay < 1 || EndDay > 31 || EndDay < 1)
        {
            return "Dzień musi być z zakresu od 1 do 31";
        }

        return string.Empty;
    }
}