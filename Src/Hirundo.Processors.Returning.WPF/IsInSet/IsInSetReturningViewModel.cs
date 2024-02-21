using CommunityToolkit.Mvvm.Input;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;
using Serilog;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Hirundo.Commons;

namespace Hirundo.Processors.Returning.WPF.IsInSet;

[ParametersData(
    typeof(IsInSetReturningCondition),
    typeof(IsInSetReturningModel),
    typeof(IsInSetReturningView),
    "Czy dane powracającego osobnika są w zbiorze?",
    "Warunek sprawdzający, czy pole danych dla osobnika powracającego znajduje się w zbiorze wartości."
)]
public class IsInSetReturningViewModel: ParametersViewModel
{
    private readonly IsInSetReturningModel _model;

    public IsInSetReturningViewModel(IsInSetReturningModel model)
    {
        _model = model;

        foreach (var value in model.Values)
        {
            AddValue(value);
        }

        Values.CollectionChanged += (_, _) => { model.SetValues(Values.Select(v => v.Value)); };
    }

    public override IDataLabelRepository Repository => _model.Repository;

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
    public override ICommand RemoveCommand => new RelayCommand(() => Remove(_model.Condition));

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