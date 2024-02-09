using System.Windows.Input;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF.IsNotEmpty;

[ParametersData(
    typeof(IsNotEmptyCondition),
    typeof(IsNotEmptyModel),
    typeof(IsNotEmptyView),
    "Czy dane nie są puste?",
    "Warunek sprawdzający, czy pole danych nie jest puste."
)]
public class IsNotEmptyViewModel(IsNotEmptyModel model) : ParametersViewModel, IRemovable
{
    public string ValueName
    {
        get => model.ValueName;
        set
        {
            model.ValueName = value;
            OnPropertyChanged();
        }
    }

    public IDataLabelRepository Repository => model.Repository;
    public ICommand RemoveCommand => new RelayCommand(() => Removed?.Invoke(this, new ParametersEventArgs(model.Condition)));

    public event EventHandler<ParametersEventArgs>? Removed;
}