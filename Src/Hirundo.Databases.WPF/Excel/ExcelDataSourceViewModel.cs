using CommunityToolkit.Mvvm.Input;
using Hirundo.Commons.Models;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Databases.Helpers;
using Serilog;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Hirundo.Databases.WPF.Excel;

[ParametersData(typeof(ExcelDatabaseParameters), typeof(ExcelDataSourceModel), typeof(ExcelDataSourceView))]
public class ExcelDataSourceViewModel(ExcelDataSourceModel model, IExcelMetadataService excelMetadataService) : ParametersViewModel(model), ILabelsUpdater
{
    public ICommand LoadedCommand => new RelayCommand(Loaded);
    public ICommand LoadFileCommand => new RelayCommand(LoadMetadata);
    public ICommand UpdateLabelsCommand => new RelayCommand(UpdateLabels);
    public ICommand RemoveValuesCommand => new RelayCommand(RemoveValues);
    public ICommand TableSelectionChangedCommand => new RelayCommand(TableSelectionChanged);
    public ICommand AddColumnCommand => new RelayCommand(AddColumn);
    public ICommand RemoveColumnCommand => new RelayCommand(RemoveColumn);
    public ICommand GetSpeciesCommand => new RelayCommand(GetSpecies);

    public IList<ColumnParameters> Columns { get; } = new ObservableCollection<ColumnParameters>(model.Columns);
    public ObservableCollection<string> DataColumns { get; } = [];
    public ObservableCollection<AccessTableMetadata> MetadatataTables { get; } = [];
    public ObservableCollection<string> Tables { get; } = [];

    public event EventHandler<EventArgs>? LabelsUpdated;

    public override string RemoveText => "Usuń źródło danych";

    public string Title { get; set; } = "Wybierz plik Excel (.xlsx)";
    public string Filter { get; set; } = "Pliki Excel (*.xlsx)|*.xlsx";

    public string Path
    {
        get => model.Path;
        set
        {
            model.Path = value;
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

    private void RemoveValues()
    {
        model.Columns.Clear();

        Columns.Clear();

        UpdateLabels();
        OnPropertyChanged(nameof(Columns));
    }

    private void UpdateLabels()
    {
        LabelsUpdated?.Invoke(this, EventArgs.Empty);
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

    private void GetSpecies()
    {
        if (string.IsNullOrEmpty(SpeciesIdentifier)) return;
        var columnData = Columns.FirstOrDefault(c => c.ValueName == SpeciesIdentifier);
        if (columnData == null) return;
        var speciesColumn = columnData.DatabaseColumn;
        if (string.IsNullOrEmpty(speciesColumn)) return;

        var speciesList = excelMetadataService.GetDistinctValues(Path, speciesColumn)
            .Select(val => val?.ToString() ?? "")
            .Where(val => !string.IsNullOrWhiteSpace(val))
            .ToArray();

        Log.Information("Pobrano listę gatunków.");

        SpeciesRepository.UpdateSpecies(speciesList);
    }

    public void Loaded()
    {
        GetSpecies();
    }

    private void LoadMetadata()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Path)) return;

            WindowHelpers.SetMouseCursor(Cursors.Wait);

            var columns = excelMetadataService.GetColumns(Path);

            Columns.Clear();
            model.Columns.Clear();

            foreach (var column in columns)
            {
                Columns.Add(column);
                DataColumns.Add(column.DatabaseColumn);
                model.Columns.Add(column);
            }

            LabelsUpdated?.Invoke(this, EventArgs.Empty);

            LabelsRepository.SetLabels(Columns.Select(c => new DataLabel(c.ValueName, c.DataType)));
        }
        catch (Exception e)
        {
            Log.Error(e, "Błąd podczas ładowania metadanych z pliku {path}.", Path);

            Log.Debug("Informacja o błędzie: {error}", e.Message);
        }
        finally
        {
            WindowHelpers.SetMouseCursor();
        }
    }

}