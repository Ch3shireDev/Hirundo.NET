using System.Windows;
using System.Windows.Input;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;

namespace Hirundo.Databases.WPF;

public class DataSourceViewModel(DataSourceModel model) : ViewModelBase
{
    private Type? selectedDataSourceType = typeof(AccessDatabaseParameters);
    public IList<ViewModelBase> DatabaseViewModels => [.. model.DatabaseParameters.Select(CreateViewModel)];

    public IList<Type> DataSourceTypes { get; } =
    [
        typeof(AccessDatabaseParameters)
    ];

    public Type? SelectedDataSourceType
    {
        get => selectedDataSourceType;
        set
        {
            selectedDataSourceType = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddNewSourceCommand => new RelayCommand(AddNewSource);

    public void AddNewSource()
    {
        if (SelectedDataSourceType is null)
        {
            return;
        }

        model.AddDataSource(SelectedDataSourceType);

        OnPropertyChanged(nameof(DatabaseViewModels));
    }

    public ViewModelBase CreateViewModel(IDatabaseParameters parameters)
    {
        var viewModel = DataSourceViewModelFactory.Create(parameters);

        if (viewModel is IRemovable<IDatabaseParameters> removable)
        {
            removable.Removed += (_, p) => { Remove(p); };
        }

        return viewModel;
    }

    private void Remove(IDatabaseParameters p)
    {
        var result = MessageBox.Show("Czy na pewno chcesz skasować bieżące źródło danych?", "Uwaga", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (result != MessageBoxResult.Yes) return;
        model.DatabaseParameters.Remove(p);
        OnPropertyChanged(nameof(DatabaseViewModels));
    }
}