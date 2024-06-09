using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Hirundo.Commons.WPF;

public class ParametersBrowserViewModel(IParametersBrowserModel parametersBrowserModel) : ObservableObject
{
    public string Header => parametersBrowserModel.Header;
    public string Title => parametersBrowserModel.Title;
    public string Description => parametersBrowserModel.Description;
    public ICommand AddParametersCommand => new RelayCommand(AddParameters);
    public string AddParametersCommandText => parametersBrowserModel.AddParametersCommandText;
    public IList<ParametersData> Options => parametersBrowserModel.ParametersDataList;
    public ParametersData? SelectedParameter { get; set; } = parametersBrowserModel.ParametersDataList.FirstOrDefault();
    public IList<ParametersViewModel> ParametersViewModels => [.. parametersBrowserModel.GetParametersViewModels().Select(SetRefreshOnUpdate)];
    public IList<CommandData> CommandList => parametersBrowserModel.CommandList;

    private ParametersViewModel SetRefreshOnUpdate(ParametersViewModel viewModel)
    {
        if (viewModel is IRemovable removable)
        {
            removable.Removed += (_, _) => OnPropertyChanged(nameof(ParametersViewModels));
        }

        return viewModel;
    }

    public void AddParameters()
    {
        if (SelectedParameter == null) return;
        parametersBrowserModel.AddParameters(SelectedParameter);
        OnPropertyChanged(nameof(ParametersViewModels));
    }
}