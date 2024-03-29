using Hirundo.Commons;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population.WPF.IsInSharedTimeWindow;

[ParametersData(
    typeof(IsInSharedTimeWindowConditionBuilder),
    typeof(IsInSharedTimeWindowModel),
    typeof(IsInSharedTimeWindowView),
    "Czy jest we współdzielonym oknie czasowym?",
     "Warunek sprawdzający, czy osobnik z populacji jest w tym samym przedziale czasowym co osobnik powracający."
    )]
public class IsInSharedTimeWindowViewModel(IsInSharedTimeWindowModel model) : ParametersViewModel(model)
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

    public DataType DataType
    {
        get => model.ValueType;
        set
        {
            model.ValueType = value;
            OnPropertyChanged();
        }
    }

    public int MaxTimeDistanceInDays
    {
        get => model.MaxTimeDistanceInDays;
        set
        {
            model.MaxTimeDistanceInDays = value;
            OnPropertyChanged();
        }
    }
}