using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Databases.WPF.Access;
using System.Windows;

namespace Hirundo.Databases.WPF;

public class DataSourceModel(IDataLabelRepository repository, IAccessMetadataService accessMetadataService) : ParametersBrowserModel<DatabaseParameters, IDatabaseParameters, DataSourceModel>(repository)
{
    private readonly IDataLabelRepository repository = repository;
    private readonly IAccessMetadataService accessMetadataService = accessMetadataService;

    public override string Description => "W tym panelu wybierasz źródło danych.";
    public override string AddParametersCommandText => "Dodaj nowe źródło danych";
    public override string Header => "Źródła danych";
    public override string Title => "Źródła danych";

    public override IList<IDatabaseParameters> Parameters { get => ParametersContainer.Databases; }

    public override IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return ParametersContainer
                .Databases
                .Select(AsParametersViewModel)
                .Select(AddUpdaterListener)
                .Select(AddRemovedListener)
            ;
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
        var mainWindow = Application.Current.MainWindow;

        if (mainWindow != null)
        {
            var result = MessageBox.Show(mainWindow, "Czy na pewno chcesz skasować bieżące źródło danych?", "Uwaga", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) return;
        }

        ParametersContainer.Databases.Remove(p);
    }

    private ParametersViewModel AsParametersViewModel(IDatabaseParameters parameters)
    {
        return parameters switch
        {
            AccessDatabaseParameters accessDatabaseParameters => new AccessDataSourceViewModel(new AccessDataSourceModel(accessDatabaseParameters, repository), accessMetadataService),
            _ => throw new NotImplementedException()
        };
    }

    public void UpdateRepository()
    {
        // TODO: Ten fragment nie jest do końca dobry, pobiera tylko różne wartości pól i unika konfliktów.
        // TODO: Należy dodać walidację zgodności dla wielu źródeł.

        var labels = new List<DataLabel>();

        foreach (var databaseParameters in ParametersContainer.Databases)
        {
            var dbLabels = databaseParameters.Columns.Select(GetDataLabel).ToList();
            labels.AddRange(dbLabels);
        }

        var groups = labels.GroupBy(l => l.Name).Select(x => x.First()).ToArray();

        repository.SetLabels(groups);
    }

    private DataLabel GetDataLabel(ColumnMapping columnMapping)
    {
        var dataType = columnMapping.DataType switch
        {
            DataValueType.ShortInt => DataType.Number,
            DataValueType.Numeric => DataType.Numeric,
            DataValueType.Undefined => DataType.Undefined,
            DataValueType.LongInt => DataType.Number,
            DataValueType.Text => DataType.Text,
            DataValueType.DateTime => DataType.Date,
            _ => DataType.Undefined
        };

        return new DataLabel(columnMapping.ValueName, dataType);
    }
}