﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Hirundo.Commons.Repositories;

namespace Hirundo.Commons.WPF;

public partial class SpeciesComboBox : UserControl, INotifyPropertyChanged
{
    public static readonly DependencyProperty SpeciesRepositoryProperty =
        DependencyProperty.Register(nameof(SpeciesRepository), typeof(ISpeciesRepository), typeof(SpeciesComboBox), new PropertyMetadata(OnSpeciesRepositoryChanged));

    public static readonly DependencyProperty SelectedSpeciesProperty =
        DependencyProperty.Register("SelectedSpecies", typeof(string), typeof(SpeciesComboBox), new PropertyMetadata(""));

    public SpeciesComboBox()
    {
        InitializeComponent();
        ComboBox.SelectedValue = SelectedSpecies;
    }

    public string SelectedSpecies
    {
        get => (string)GetValue(SelectedSpeciesProperty);
        set => SetValue(SelectedSpeciesProperty, value);
    }

    public ISpeciesRepository? SpeciesRepository
    {
        get => (ISpeciesRepository)GetValue(SpeciesRepositoryProperty);
        set => SetValue(SpeciesRepositoryProperty, value);
    }


    private IList<string> Species { get; set; } = [];

    public event PropertyChangedEventHandler? PropertyChanged;

    private static void OnSpeciesRepositoryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
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

    private void OnSpeciesChanged(object? sender, EventArgs e)
    {
        OnSpeciesChanged();
    }

    private void OnSpeciesChanged()
    {
        if (SpeciesRepository is null) return;
        var selectedSpecies = SelectedSpecies;
        Species = [.. SpeciesRepository.GetSpecies()];
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