using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Commons.Repositories.Labels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Hirundo.Commons.WPF;

public partial class LabelsComboBox : UserControl, INotifyPropertyChanged
{
    public string ValueLabel
    {
        get { return (string)GetValue(ValueLabelProperty); }
        set { SetValue(ValueLabelProperty, value); }
    }

    public static readonly DependencyProperty ValueLabelProperty =
        DependencyProperty.Register(nameof(ValueLabel), typeof(string), typeof(LabelsComboBox), new PropertyMetadata("Nazwa wartości"));

    public static readonly DependencyProperty LabelsRepositoryProperty =
        DependencyProperty.Register(nameof(LabelsRepository), typeof(ILabelsRepository), typeof(LabelsComboBox), new PropertyMetadata(OnDataLabelRepositoryChanged));

    public static readonly DependencyProperty ValueNameProperty =
        DependencyProperty.Register(
            nameof(ValueName),
            typeof(string),
            typeof(LabelsComboBox),
            new FrameworkPropertyMetadata(
                string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnValueNameChanged
            )
        );

    public static readonly DependencyProperty DataTypeProperty =
        DependencyProperty.Register(
            nameof(DataType),
            typeof(DataType),
            typeof(LabelsComboBox),
            new FrameworkPropertyMetadata(
                DataType.Undefined,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
            )
        );

    public LabelsComboBox()
    {
        InitializeComponent();
        ComboBox.DisplayMemberPath = nameof(DataLabel.Name);
        ComboBox.SelectedValue = SelectedLabel;
    }

    public DataType DataType
    {
        get => (DataType)GetValue(DataTypeProperty);
        set => SetValue(DataTypeProperty, value);
    }

    public ILabelsRepository? LabelsRepository
    {
        get => (ILabelsRepository)GetValue(LabelsRepositoryProperty);
        set => SetValue(LabelsRepositoryProperty, value);
    }

    public string? ValueName
    {
        get => (string)GetValue(ValueNameProperty);
        set => SetValue(ValueNameProperty, value);
    }

    public DataLabel? SelectedLabel
    {
        get => Labels.FirstOrDefault(l => l.Name == ValueName);
        set
        {
            ValueName = value?.Name ?? string.Empty;
            DataType = value?.DataType ?? DataType.Undefined;
            OnPropertyChanged();
        }
    }

    private IList<DataLabel> Labels { get; set; } = [];

    public event PropertyChangedEventHandler? PropertyChanged;

    private static void OnDataLabelRepositoryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not LabelsComboBox comboBox) return;

        if (e.NewValue is ILabelsRepository repository)
        {
            repository.LabelsChanged -= comboBox.OnLabelsChanged;
            repository.LabelsChanged += comboBox.OnLabelsChanged;
            comboBox.OnLabelsChanged();
        }
        else
        {
            comboBox.ComboBox.ItemsSource = null;
        }
    }

    private static void OnValueNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not LabelsComboBox) return;
    }

    private void OnLabelsChanged(object? sender, EventArgs e)
    {
        OnLabelsChanged();
    }

    private void OnLabelsChanged()
    {
        if (LabelsRepository is null) return;
        var valueName = ValueName;
        Labels = [.. LabelsRepository.GetLabels()];
        SelectedLabel = Labels.FirstOrDefault(l => l.Name == valueName);
        ComboBox.ItemsSource = Labels;
        ComboBox.SelectedValue = SelectedLabel;
        ValueName = valueName;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}