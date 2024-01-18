using System.Windows.Input;
using Hirundo.Commons.WPF.Helpers;

namespace Hirundo.Commons.WPF;

public class ConditionsBrowserViewModel(IBrowserModel browserModel) : ViewModelBase
{
    public ICommand AddConditionCommand => new RelayCommand(AddCondition);
    public string Description => browserModel.Description;
    public string Title => browserModel.Title;
    public IList<SettingsData> Options => browserModel.Options;
    public SettingsData? SelectedOption { get; set; } = browserModel.Options.FirstOrDefault();
    public IList<ConditionViewModel> ConditionViewModels => [.. browserModel.GetConditions().Select(SetRefreshOnUpdate)];

    private ConditionViewModel SetRefreshOnUpdate(ConditionViewModel viewModelBase)
    {
        if (viewModelBase is IRemovable removable)
        {
            removable.Removed += (_, _) => OnPropertyChanged(nameof(ConditionViewModels));
        }

        return viewModelBase;
    }

    public void AddCondition()
    {
        if (SelectedOption == null) return;
        browserModel.AddCondition(SelectedOption);
        OnPropertyChanged(nameof(ConditionViewModels));
    }
}