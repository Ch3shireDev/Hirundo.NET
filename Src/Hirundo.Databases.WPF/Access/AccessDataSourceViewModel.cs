using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Databases.Conditions;
using Serilog;

namespace Hirundo.Databases.WPF.Access;

public class AccessDataSourceViewModel(AccessDatabaseParameters parameters, IAccessMetadataService accessMetadataService) : ParametersViewModel, IRemovable, ILabelsUpdater
{
    public string Path
    {
        get => parameters.Path;
        set
        {
            parameters.Path = value;
            OnPropertyChanged();
        }
    }

    public string Table
    {
        get => parameters.Table;
        set
        {
            parameters.Table = value;
            SetDataColumns(value);
            OnPropertyChanged();
        }
    }

    public ObservableCollection<string> DataColumns { get; } = new();
    public ICommand AfterFileCommand => new RelayCommand(LoadMetadata);
    public ICommand UpdateLabelsCommand => new RelayCommand(UpdateLabels);
    public IList<DatabaseCondition> Conditions => parameters.Conditions;
    public IList<ColumnMapping> Columns => parameters.Columns;
    public ICommand LoadedCommand => new RelayCommand(LoadMetadata);

    public ObservableCollection<AccessTableMetadata> MetadatataTables { get; } = new();
    public ObservableCollection<string> Tables { get; } = new();
    public ICommand RemoveValuesCommand => new RelayCommand(RemoveValues);

    public event EventHandler<EventArgs>? LabelsUpdated;
    public ICommand RemoveCommand => new RelayCommand(RemoveDataSource);

    public event EventHandler<ParametersEventArgs>? Removed;


    private void RemoveValues()
    {
        var dialog = MessageBox.Show(
            "Ta operacja skasuje wybór kolumn. Baza danych pozostanie niezmieniona.",
            "Usuwanie wybranych kolumn",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning
        );

        if (dialog == MessageBoxResult.Yes)
        {
            parameters.Columns.Clear();
            parameters.Conditions.Clear();
        }
    }

    private void SetDataColumns(string tableName)
    {
        var columns = MetadatataTables
            .FirstOrDefault(table => table.TableName == tableName)
            ?.Columns
            .Select(column => column.ColumnName)
            .ToArray() ?? [];

        DataColumns.Clear();

        foreach (var column in columns)
        {
            DataColumns.Add(column);
        }
    }

    private void UpdateLabels()
    {
        LabelsUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void LoadMetadata()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Path)) return;
            var metadata = accessMetadataService.GetTables(Path).ToArray();
            Log.Information($"Załadowano metadane: {metadata.Length} tabel.");

            var selectedTable = Table;

            Tables.Clear();

            foreach (var table in metadata)
            {
                MetadatataTables.Add(table);
                Tables.Add(table.TableName);
            }

            Table = Tables
                .FirstOrDefault(
                    t => string.Equals(t, selectedTable, StringComparison.InvariantCultureIgnoreCase)
                ) ?? string.Empty;
        }
        catch (Exception e)
        {
            Log.Error(e, $"Błąd podczas ładowania metadanych z pliku {Path}.");

            if (!string.IsNullOrEmpty(Table))
            {
                Tables.Add(Table);
            }
        }
    }

    public void RemoveDataSource()
    {
        Removed?.Invoke(this, new ParametersEventArgs(parameters));
    }
}