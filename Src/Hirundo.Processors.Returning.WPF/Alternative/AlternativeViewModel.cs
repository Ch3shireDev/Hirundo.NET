using System.Collections.ObjectModel;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.Alternative;

[ParametersData(
    typeof(AlternativeReturningCondition),
    typeof(AlternativeModel),
    typeof(AlternativeView)
)]
public class AlternativeViewModel(AlternativeModel model) : ParametersViewModel(model)
{
    public IList<ParametersData> Options { get; } = new ObservableCollection<ParametersData>(model.AvailableParameters);

    public ParametersData? FirstParameter
    {
        get => model.FirstParameter;
        set
        {
            model.FirstParameter = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(FirstViewModel));
        }
    }

    public ParametersViewModel? FirstViewModel => model.FirstViewModel;

    public ParametersData? SecondParameter
    {
        get => model.SecondParameter;
        set
        {
            model.SecondParameter = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(SecondViewModel));
        }
    }

    public ParametersViewModel? SecondViewModel => model.SecondViewModel;
}