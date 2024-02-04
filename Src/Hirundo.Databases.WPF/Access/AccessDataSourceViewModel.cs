using System.Collections.ObjectModel;
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
    public ObservableCollection<AccessTableMetadata> MetadatataTables { get; } = new();
    public ObservableCollection<string> Tables { get; } = new();

    public event EventHandler<EventArgs>? LabelsUpdated;

    public event EventHandler<ParametersEventArgs>? Removed;


    private void RemoveValues()
    {
        parameters.Columns.Clear();
        parameters.Conditions.Clear();

        Columns.Clear();
        Conditions.Clear();

        UpdateLabels();
        OnPropertyChanged(nameof(Columns));
        OnPropertyChanged(nameof(Conditions));
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

    private void LoadMetadata(bool force = false)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Path)) return;
            if (!force && Tables.Any()) return;
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

    public void TableSelectionChanged()
    {
        RemoveValues();
    }

    public void AddColumn()
    {
        var newColumnMapping = new ColumnMapping();
        parameters.Columns.Add(newColumnMapping);
        Columns.Add(newColumnMapping);
    }

    public void RemoveColumn()
    {
        if (Columns.Count <= 0) return;
        var column = Columns.Last();
        parameters.Columns.Remove(column);
        Columns.Remove(column);
        UpdateLabels();
    }

    private void AddCondition()
    {
        var newCondition = new DatabaseCondition();
        parameters.Conditions.Add(newCondition);
        Conditions.Add(newCondition);
    }


    private void RemoveCondition()
    {
        if (Conditions.Count <= 0) return;
        var condition = Conditions.Last();
        parameters.Conditions.Remove(condition);
        Conditions.Remove(condition);
    }

    #region commands

    public ICommand LoadedCommand => new RelayCommand(()=>LoadMetadata(false));
    public ICommand AfterFileCommand => new RelayCommand(()=>LoadMetadata(true));
    public ICommand UpdateLabelsCommand => new RelayCommand(UpdateLabels);
    public ICommand RemoveCommand => new RelayCommand(RemoveDataSource);
    public ICommand RemoveValuesCommand => new RelayCommand(RemoveValues);
    public ICommand TableSelectionChangedCommand => new RelayCommand(TableSelectionChanged);
    public ICommand AddColumnCommand => new RelayCommand(AddColumn);
    public ICommand RemoveColumnCommand => new RelayCommand(RemoveColumn);
    public ICommand AddConditionCommand => new RelayCommand(AddCondition);
    public ICommand RemoveConditionCommand => new RelayCommand(RemoveCondition);

    #endregion

    #region collections

    public IList<ColumnMapping> Columns { get; } = new ObservableCollection<ColumnMapping>(parameters.Columns);
    public IList<DatabaseCondition> Conditions { get; } = new ObservableCollection<DatabaseCondition>(parameters.Conditions);

    #endregion
}