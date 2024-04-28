using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Hirundo.Commons.WPF;
using Hirundo.Databases.Conditions;
using Serilog;

namespace Hirundo.Databases.WPF.Access;

[ParametersData(typeof(AccessDatabaseParameters), typeof(AccessDataSourceModel), typeof(AccessDataSourceView))]
public class AccessDataSourceViewModel(AccessDataSourceModel model, IAccessMetadataService accessMetadataService) : ParametersViewModel(model), ILabelsUpdater
{
    public override string Name => "Źródło danych Access";
    public override string Description => "Źródło danych z pliku Access.";
    public override string RemoveText => "Usuń źródło danych";

    public string Path
    {
        get => model.Path;
        set
        {
            model.Path = value;
            OnPropertyChanged();
        }
    }

    public string Table
    {
        get => model.Table;
        set
        {
            model.Table = value;
            SetDataColumns(value);
            OnPropertyChanged();
        }
    }

    public string SpeciesIdentifier
    {
        get => model.SpeciesIdentifier;
        set
        {
            model.SpeciesIdentifier = value;
            OnPropertyChanged();
        }
    }

    public string RingIdentifier
    {
        get => model.RingIdentifier;
        set
        {
            model.RingIdentifier = value;
            OnPropertyChanged();
        }
    }

    public string DateIdentifier
    {
        get => model.DateIdentifier;
        set
        {
            model.DateIdentifier = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<string> DataColumns { get; } = [];
    public ObservableCollection<AccessTableMetadata> MetadatataTables { get; } = [];
    public ObservableCollection<string> Tables { get; } = [];

    public event EventHandler<EventArgs>? LabelsUpdated;


    private void RemoveValues()
    {
        model.Columns.Clear();
        model.Conditions.Clear();

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
            Log.Debug($"Załadowano metadane: {metadata.Length} tabel.");

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

            Log.Debug("Informacja o błędzie: {error}", e.Message);

            if (!string.IsNullOrEmpty(Table))
            {
                Tables.Add(Table);
            }
        }
    }

    public void TableSelectionChanged()
    {
        RemoveValues();
    }

    public void AddColumn()
    {
        var newColumnMapping = new ColumnParameters();
        model.Columns.Add(newColumnMapping);
        Columns.Add(newColumnMapping);
    }

    public void RemoveColumn()
    {
        if (Columns.Count <= 0) return;
        var column = Columns.Last();
        model.Columns.Remove(column);
        Columns.Remove(column);
        UpdateLabels();
    }

    public void AddCondition()
    {
        var newCondition = new DatabaseCondition();
        model.Conditions.Add(newCondition);
        Conditions.Add(newCondition);
    }

    public void RemoveCondition()
    {
        if (Conditions.Count <= 0) return;
        var condition = Conditions.Last();
        model.Conditions.Remove(condition);
        Conditions.Remove(condition);
    }

    private void GetSpecies()
    {
        if (string.IsNullOrEmpty(SpeciesIdentifier)) return;
        if (string.IsNullOrEmpty(Table)) return;
        var columnData = Columns.FirstOrDefault(c => c.ValueName == SpeciesIdentifier);
        if (columnData == null) return;
        if (columnData.DataType != DataValueType.Text) return;
        var speciesColumn = columnData.DatabaseColumn;
        if (string.IsNullOrEmpty(speciesColumn)) return;

        var speciesList = accessMetadataService.GetDistinctValues(Path, Table, speciesColumn)
            .Select(val => val?.ToString() ?? "")
            .Where(val => !string.IsNullOrWhiteSpace(val))
            .ToArray();
        Log.Information("Pobrano listę gatunków: {species}", string.Join(", ", speciesList));
        SpeciesRepository.UpdateSpecies(speciesList);
    }

    #region commands

    public ICommand LoadedCommand => new RelayCommand(() => LoadMetadata());
    public ICommand AfterFileCommand => new RelayCommand(() => LoadMetadata(true));
    public ICommand UpdateLabelsCommand => new RelayCommand(UpdateLabels);
    public ICommand RemoveValuesCommand => new RelayCommand(RemoveValues);
    public ICommand TableSelectionChangedCommand => new RelayCommand(TableSelectionChanged);
    public ICommand AddColumnCommand => new RelayCommand(AddColumn);
    public ICommand RemoveColumnCommand => new RelayCommand(RemoveColumn);
    public ICommand AddConditionCommand => new RelayCommand(AddCondition);
    public ICommand RemoveConditionCommand => new RelayCommand(RemoveCondition);
    public ICommand GetSpeciesCommand => new RelayCommand(GetSpecies);

    #endregion

    #region collections

    public IList<ColumnParameters> Columns { get; } = new ObservableCollection<ColumnParameters>(model.Columns);
    public IList<DatabaseCondition> Conditions { get; } = new ObservableCollection<DatabaseCondition>(model.Conditions);

    #endregion
}