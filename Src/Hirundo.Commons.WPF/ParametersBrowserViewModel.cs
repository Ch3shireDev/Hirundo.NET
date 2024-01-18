using System.Windows.Input;
using Hirundo.Commons.WPF.Helpers;

namespace Hirundo.Commons.WPF;

public class ParametersBrowserViewModel(IParametersBrowserModel parametersBrowserModel) : ViewModelBase
{
    public ICommand AddParametersCommand => new RelayCommand(AddParameters);
    public string Description => parametersBrowserModel.Description;
    public string Title => parametersBrowserModel.Title;
    public IList<ParametersData> Options => parametersBrowserModel.ParametersDataList;
    public ParametersData? SelectedParameter { get; set; } = parametersBrowserModel.ParametersDataList.FirstOrDefault();
    public IList<ParametersViewModel> ParametersViewModels => [.. parametersBrowserModel.GetParametersViewModels().Select(SetRefreshOnUpdate)];

    private ParametersViewModel SetRefreshOnUpdate(ParametersViewModel viewModelBase)
    {
        if (viewModelBase is IRemovable removable)
        {
            removable.Removed += (_, _) => OnPropertyChanged(nameof(ParametersViewModels));
        }

        return viewModelBase;
    }

    public void AddParameters()
    {
        if (SelectedParameter == null) return;
        parametersBrowserModel.AddParameters(SelectedParameter);
        OnPropertyChanged(nameof(ParametersViewModels));
    }
}