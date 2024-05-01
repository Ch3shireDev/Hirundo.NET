using CommunityToolkit.Mvvm.Input;
using Hirundo.Commons.Models;
using Hirundo.Commons.WPF;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Hirundo.Databases.WPF.Excel;

//[ParametersData(typeof(XlsxDatabaseParameters), typeof(ExcelDataSourceModel), typeof(ExcelDataSourceView))]
public class ExcelDataSourceViewModel(ExcelDataSourceModel model, IAccessMetadataService accessMetadataService) : ParametersViewModel(model), ILabelsUpdater
{
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
        if (columnData.DataType != DataType.Text) return;
        var speciesColumn = columnData.DatabaseColumn;
        if (string.IsNullOrEmpty(speciesColumn)) return;

        //var speciesList = accessMetadataService.GetDistinctValues(Path, Table, speciesColumn)
        //    .Select(val => val?.ToString() ?? "")
        //    .Where(val => !string.IsNullOrWhiteSpace(val))
        //    .ToArray();

        //Log.Information("Pobrano listę gatunków.");
        //Log.Debug("Lista gatunków: {species}", string.Join(", ", speciesList));

        //SpeciesRepository.UpdateSpecies(speciesList);
    }

    public void Loaded()
    {
        GetSpecies();
    }

    #region commands
    public ICommand LoadedCommand => new RelayCommand(Loaded);
    public ICommand UpdateLabelsCommand => new RelayCommand(UpdateLabels);
    public ICommand RemoveValuesCommand => new RelayCommand(RemoveValues);
    public ICommand TableSelectionChangedCommand => new RelayCommand(TableSelectionChanged);
    public ICommand AddColumnCommand => new RelayCommand(AddColumn);
    public ICommand RemoveColumnCommand => new RelayCommand(RemoveColumn);
    public ICommand GetSpeciesCommand => new RelayCommand(GetSpecies);
    #endregion

    #region collections
    public IList<ColumnParameters> Columns { get; } = new ObservableCollection<ColumnParameters>(model.Columns);
    #endregion
}