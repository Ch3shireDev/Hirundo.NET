using Hirundo.Commons;
using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Population.WPF.IsInSharedTimeWindow;

public class IsInSharedTimeWindowViewModel(IsInSharedTimeWindowModel model) : ParametersViewModel(model)
{
    public override string Name => "Czy jest we współdzielonym oknie czasowym?";
    public override string Description => "Warunek sprawdzający, czy osobnik z populacji jest w tym samym przedziale czasowym co osobnik powracający.";

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