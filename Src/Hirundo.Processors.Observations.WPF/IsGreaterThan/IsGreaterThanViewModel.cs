using Hirundo.Commons;
using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.IsGreaterThan;

[ParametersData(
    typeof(IsGreaterThanCondition),
    typeof(IsGreaterThanModel),
    typeof(IsGreaterThanView),
    "Czy wartość jest większa niż?",
    "Warunek sprawdzający, czy pole danych jest większe niż podana wartość. Daty należy podawać w formie YYYY-MM-dd."
)]
public class IsGreaterThanViewModel(IsGreaterThanModel model) : ParametersViewModel(model)
{
    public IsGreaterThanModel Model => model;

    public object? Value
    {
        get => model.Value;
        set
        {
            model.Value = value;
            OnPropertyChanged();
        }
    }

    public string ValueName
    {
        get => model.ValueName;
        set
        {
            model.ValueName = value;
            OnPropertyChanged();
        }
    }


    public DataType DataType { get; set; }
}