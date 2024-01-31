﻿using System.Windows.Input;
using Hirundo.Commons.WPF.Helpers;

namespace Hirundo.Commons.WPF;

public class ParametersBrowserViewModel(ParametersBrowserModel parametersBrowserModel) : ViewModelBase
{
    public ICommand AddParametersCommand => new RelayCommand(AddParameters);
    public string Description => parametersBrowserModel.Description;
    public string Title => parametersBrowserModel.Title;
    public string Header => parametersBrowserModel.Header;
    public string AddParametersCommandText => parametersBrowserModel.AddParametersCommandText;
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