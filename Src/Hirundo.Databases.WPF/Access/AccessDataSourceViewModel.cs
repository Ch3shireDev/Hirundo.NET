using System.Windows.Input;
using Hirundo.Commons;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Databases.Conditions;
using Hirundo.Repositories.DataLabels;
using Serilog;

namespace Hirundo.Databases.WPF.Access;

public class AccessDataSourceViewModel(AccessDatabaseParameters parameters, IDataLabelRepository repository) : ParametersViewModel, IRemovable
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
            OnPropertyChanged();
        }
    }

    public IList<DatabaseCondition> Conditions => parameters.Conditions;
    public IList<ColumnMapping> Columns => parameters.Columns;
    public ICommand UpdateLabelsCommand => new RelayCommand(UpdateLabels);
    public ICommand RemoveCommand => new RelayCommand(RemoveDataSource);

    public event EventHandler<ParametersEventArgs>? Removed;

    private void UpdateLabels()
    {
        Log.Information("UpdateLabels");
        var labels = parameters
            .Columns
            .Select(GetDataLabel)
            .ToList();

        repository.UpdateLabels(labels);
    }

    private DataLabel GetDataLabel(ColumnMapping columnMapping)
    {
        var name = columnMapping.ValueName;

        var dataType = columnMapping.DataType switch
        {
            DataValueType.Text => DataType.Text,
            _ => DataType.Text
        };

        return new DataLabel(name, dataType);
    }

    public void RemoveDataSource()
    {
        Removed?.Invoke(this, new ParametersEventArgs(parameters));
    }
}