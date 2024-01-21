using System.Windows;
using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Databases.WPF.Access;

namespace Hirundo.Databases.WPF;

public class DataSourceModel(IDataLabelRepository dataLabelRepository) : IParametersBrowserModel
{
    public IList<IDatabaseParameters> DatabaseParameters { get; } = new List<IDatabaseParameters>();

    public string Description => "W tym panelu wybierasz źródło danych.";
    public string AddParametersCommandText => "Dodaj nowe źródło danych";
    public string Header => "Źródła danych";
    public string Title => "Źródła danych";

    public IList<ParametersData> ParametersDataList { get; } =
    [
        new ParametersData(typeof(AccessDatabaseParameters), "Baza danych Access (*.mdb)", "Źródło danych wyszczególniające tabelę bazy danych Access")
    ];

    public void AddParameters(ParametersData parametersData)
    {
        AddDatasource(parametersData.Type);
    }

    public IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return DatabaseParameters
                .Select(AsParametersViewModel)
                .Select(AddUpdaterListener)
                .Select(AddRemovedListener)
            ;
    }

    public void AddDatasource(Type selectedDataSourceType)
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

    private ParametersViewModel AddUpdaterListener(ParametersViewModel viewModel)
    {
        if (viewModel is ILabelsUpdater labelsUpdater)
        {
            labelsUpdater.LabelsUpdated += (_, _) => { UpdateRepository(); };
        }

        return viewModel;
    }

    private ParametersViewModel AddRemovedListener(ParametersViewModel viewModel)
    {
        if (viewModel is IRemovable removable)
        {
            removable.Removed += (_, p) =>
            {
                if (p.Parameters is IDatabaseParameters parametersToRemove)
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

    private static ParametersViewModel AsParametersViewModel(IDatabaseParameters parameters)
    {
        return parameters switch
        {
            AccessDatabaseParameters accessDatabaseParameters => new AccessDataSourceViewModel(accessDatabaseParameters),
            _ => throw new NotImplementedException()
        };
    }

    public void UpdateRepository()
    {
        // TODO: Ten fragment nie jest do końca dobry, pobiera tylko różne wartości pól i unika konfliktów.
        // Należy dodać walidację zgodności dla wielu źródeł.

        var labels = new List<DataLabel>();

        foreach (var databaseParameters in DatabaseParameters)
        {
            var dbLabels = databaseParameters.Columns.Select(GetDataLabel).ToList();
            labels.AddRange(dbLabels);
        }

        var groups = labels.GroupBy(l => l.Name).Select(x => x.First()).ToArray();

        dataLabelRepository.UpdateLabels(groups);
    }

    private DataLabel GetDataLabel(ColumnMapping columnMapping)
    {
        var dataType = columnMapping.DataType switch
        {
            DataValueType.ShortInt => DataType.Integer,
            DataValueType.Numeric => DataType.Numeric,
            DataValueType.Undefined => DataType.Undefined,
            DataValueType.LongInt => DataType.Integer,
            DataValueType.Text => DataType.Text,
            DataValueType.DateTime => DataType.Date,
            _ => DataType.Undefined
        };

        return new DataLabel(columnMapping.ValueName, dataType);
    }
}