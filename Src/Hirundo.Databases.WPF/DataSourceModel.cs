using System.Windows;
using Hirundo.Commons.WPF;
using Hirundo.Databases.WPF.Access;

namespace Hirundo.Databases.WPF;

public class DataSourceModel : IBrowserModel
{
    public IList<IDatabaseParameters> DatabaseParameters { get; } = new List<IDatabaseParameters>();

    public string Description { get; } = "W tym panelu wybierasz źródło danych.";
    public string Title { get; } = "Źródła danych";

    public IList<SettingsData> Options { get; } =
    [
        new SettingsData(typeof(AccessDatabaseParameters), "Baza danych Access (*.mdb)", "Źródło danych wyszczególniające tabelę bazy danych Access")
    ];

    public void AddCondition(SettingsData settingsData)
    {
        AddCondition(settingsData.Type);
    }

    public IEnumerable<ConditionViewModel> GetConditions()
    {
        return DatabaseParameters.Select(CreateViewModel);
    }

    public void AddCondition(Type selectedDataSourceType)
    {
        switch (selectedDataSourceType)
        {
            case { } accessDatabaseParametersType when accessDatabaseParametersType == typeof(AccessDatabaseParameters):
                DatabaseParameters.Add(new AccessDatabaseParameters());
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private ConditionViewModel CreateViewModel(IDatabaseParameters parameters)
    {
        var viewModel = Create(parameters);

        if (viewModel is IRemovable removable)
        {
            removable.Removed += (_, p) =>
            {
                if (p.Condition is IDatabaseParameters parametersToRemove)
                {
                    Remove(parametersToRemove);
                }
            };
        }

        return viewModel;
    }

    private void Remove(IDatabaseParameters p)
    {
        var result = MessageBox.Show("Czy na pewno chcesz skasować bieżące źródło danych?", "Uwaga", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (result != MessageBoxResult.Yes) return;
        DatabaseParameters.Remove(p);
    }

    public static ConditionViewModel Create(IDatabaseParameters parameters)
    {
        return parameters switch
        {
            AccessDatabaseParameters accessDatabaseParameters => new AccessDataSourceViewModel(accessDatabaseParameters),
            _ => throw new NotImplementedException()
        };
    }
}