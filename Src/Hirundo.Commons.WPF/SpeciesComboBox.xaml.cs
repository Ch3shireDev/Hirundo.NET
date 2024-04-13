using Hirundo.Commons.Repositories;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Hirundo.Commons.WPF;

public partial class SpeciesComboBox : UserControl, INotifyPropertyChanged
{
    public static readonly DependencyProperty SpeciesRepositoryProperty =
        DependencyProperty.Register(nameof(SpeciesRepository), typeof(ISpeciesRepository), typeof(SpeciesComboBox), new PropertyMetadata(OnDataLabelRepositoryChanged));

    public static readonly DependencyProperty SelectedSpeciesProperty =
        DependencyProperty.Register("SelectedSpecies", typeof(string), typeof(SpeciesComboBox), new PropertyMetadata(""));

    public string SelectedSpecies
    {
        get { return (string)GetValue(SelectedSpeciesProperty); }
        set { SetValue(SelectedSpeciesProperty, value); }
    }

    public SpeciesComboBox()
    {
        InitializeComponent();
        ComboBox.SelectedValue = SelectedSpecies;
    }

    public ISpeciesRepository? SpeciesRepository
    {
        get => (ISpeciesRepository)GetValue(SpeciesRepositoryProperty);
        set => SetValue(SpeciesRepositoryProperty, value);
    }


    private IList<string> Species { get; set; } = [];

    public event PropertyChangedEventHandler? PropertyChanged;

    private static void OnDataLabelRepositoryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not SpeciesComboBox comboBox) return;

        if (e.NewValue is ISpeciesRepository speciesRepository)
        {
            speciesRepository.SpeciesChanged -= comboBox.OnSpeciesChanged;
            speciesRepository.SpeciesChanged += comboBox.OnSpeciesChanged;
            comboBox.OnSpeciesChanged();
        }
        else
        {
            comboBox.ComboBox.ItemsSource = null;
        }
    }

    private static void OnValueNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not SpeciesComboBox) return;
    }

    private void OnSpeciesChanged(object? sender, EventArgs e)
    {
        OnSpeciesChanged();
    }

    private void OnSpeciesChanged()
    {
        if (SpeciesRepository is null) return;
        var selectedSpecies = SelectedSpecies;
        Species = [.. SpeciesRepository.GetSpecies()];
        SelectedSpecies = Species.FirstOrDefault() ?? "";
        ComboBox.ItemsSource = Species;
        ComboBox.SelectedValue = SelectedSpecies;
        SelectedSpecies = selectedSpecies;
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