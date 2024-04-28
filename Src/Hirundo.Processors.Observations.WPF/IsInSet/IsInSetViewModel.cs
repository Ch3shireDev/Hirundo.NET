using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Hirundo.Commons.Models;
using Hirundo.Commons.WPF;
using Serilog;

namespace Hirundo.Processors.Observations.WPF.IsInSet;

[ParametersData(
    typeof(IsInSetCondition),
    typeof(IsInSetModel),
    typeof(IsInSetView)
)]
public class IsInSetViewModel : ParametersViewModel
{
    private readonly IsInSetModel _model;

    public IsInSetViewModel(IsInSetModel model) : base(model)
    {
        _model = model;

        foreach (var value in model.Values)
        {
            AddValue(value);
        }

        Values.CollectionChanged += (_, _) => { model.SetValues(Values.Select(v => v.Value)); };
    }

    public string ValueName
    {
        get => _model.ValueName;
        set
        {
            _model.ValueName = value;
            OnPropertyChanged();
        }
    }

    public DataType ValueType
    {
        get => _model.ValueType;
        set
        {
            _model.ValueType = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<ValueContainer> Values { get; set; } = [];
    public ICommand AddValueCommand => new RelayCommand(AddValue);
    public ICommand RemoveValueCommand => new RelayCommand(RemoveValue);

    public void AddValue()
    {
        AddValue(null);
    }

    private void AddValue(object? value)
    {
        var valueStr = value?.ToString() ?? string.Empty;

        var containerValue = new ValueContainer(valueStr);

        containerValue.PropertyChanged += (_, _) =>
        {
            var index = Values.IndexOf(containerValue);
            if (index == -1) return;
            _model.SetValue(containerValue.Value, index);

            Log.Information($"Property update: {containerValue.Value}");
        };

        Values.Add(containerValue);
    }

    public void RemoveValue()
    {
        if (!Values.Any()) return;
        Values.Remove(Values.Last());
    }
}