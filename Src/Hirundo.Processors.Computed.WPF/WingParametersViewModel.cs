using CommunityToolkit.Mvvm.Input;
using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Hirundo.Processors.Computed.WPF;


public class WingParametersViewModel<T> : ParametersViewModel where T : WingParametersBase
{
    private readonly WingParametersModel<T> _model;

    public WingParametersViewModel(WingParametersModel<T> model)
    {
        _model = model;
        WingParameters = new ObservableCollection<ValueContainer>(_model.WingParameters.Select(p => new ValueContainer(p)));

        WingParameters.CollectionChanged += (s, e) =>
        {
            _model.WingParameters = WingParameters
                .Select(p => p.Value)
                .ToArray();
        };
    }

    public string ResultName
    {
        get => _model.ResultName;
        set
        {
            _model.ResultName = value;
            OnPropertyChanged();
        }
    }

    public string WingName
    {
        get => _model.WingName;
        set
        {
            _model.WingName = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<ValueContainer> WingParameters { get; }
    public DataType WingType { get; set; }
    public override ICommand RemoveCommand => new RelayCommand(() => Remove(_model.ComputedValue));
    public override IDataLabelRepository Repository => _model.Repository;
    public ICommand UpdateLabelCommand => new RelayCommand(_model.UpdateLabel);
    public ICommand LoadCommand => new RelayCommand(_model.UpdateLabel);

    public string D2Name
    {
        get => _model.GetDName(2);
        set
        {
            _model.SetDName(2, value);
            OnPropertyChanged();
        }
    }

    public string D3Name
    {
        get => _model.GetDName(3);
        set
        {
            _model.SetDName(3, value);
            OnPropertyChanged();
        }
    }

    public string D4Name
    {
        get => _model.GetDName(4);
        set
        {
            _model.SetDName(4, value);
            OnPropertyChanged();
        }
    }

    public string D5Name
    {
        get => _model.GetDName(5);
        set
        {
            _model.SetDName(5, value);
            OnPropertyChanged();
        }
    }

    public string D6Name
    {
        get => _model.GetDName(6);
        set
        {
            _model.SetDName(6, value);
            OnPropertyChanged();
        }
    }

    public string D7Name
    {
        get => _model.GetDName(7);
        set
        {
            _model.SetDName(7, value);
            OnPropertyChanged();
        }
    }

    public string D8Name
    {
        get => _model.GetDName(8);
        set
        {
            _model.SetDName(8, value);
            OnPropertyChanged();
        }
    }


    protected override void Remove(object parameters)
    {
        base.Remove(parameters);
        _model.RemoveLabel();
    }
}