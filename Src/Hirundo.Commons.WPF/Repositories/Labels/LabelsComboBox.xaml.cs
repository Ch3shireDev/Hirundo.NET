﻿using System.Windows;
using System.Windows.Controls;
using Hirundo.Commons.Repositories.Labels;

namespace Hirundo.Commons.WPF.Repositories.Labels;

public partial class LabelsComboBox : UserControl
{
    public static readonly DependencyProperty RepositoryProperty =
        DependencyProperty.Register(nameof(Repository), typeof(IDataLabelRepository), typeof(LabelsComboBox), new PropertyMetadata(OnDataLabelRepositoryChanged));

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

    public IDataLabelRepository? Repository
    {
        get => (IDataLabelRepository)GetValue(RepositoryProperty);
        set => SetValue(RepositoryProperty, value);
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
        }
    }

    private IList<DataLabel> Labels { get; set; } = new List<DataLabel>();

    private static void OnDataLabelRepositoryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not LabelsComboBox comboBox) return;

        if (e.NewValue is IDataLabelRepository repository)
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
        if (d is not LabelsComboBox comboBox) return;
    }

    private void OnLabelsChanged(object? sender, EventArgs e)
    {
        OnLabelsChanged();
    }

    private void OnLabelsChanged()
    {
        if (Repository is null) return;
        Labels = [..Repository.GetLabels()];
        ComboBox.ItemsSource = Labels;
        ComboBox.SelectedValue = SelectedLabel;
    }
}