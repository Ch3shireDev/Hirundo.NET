﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Serialization.Json;
using Hirundo.Writers;
using Hirundo.Writers.WPF;
using Microsoft.Win32;
using Serilog;
using Serilog.Events;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Hirundo.App.WPF.Components;

public sealed class MainViewModel : ObservableObject
{
    private readonly MainModel _model;
    private ObservableObject? _selectedViewModel;

    private bool isProcessing;

    public MainViewModel(MainModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        _model = model;

        _model.SetIsProcessing = (value) => { IsProcessing = value; };

        DataSourceViewModel = new ParametersBrowserViewModel(model.DatabasesBrowserModel);
        ComputedValuesViewModel = new ParametersBrowserViewModel(model.ComputedValuesModel);
        ObservationsViewModel = new ParametersBrowserViewModel(model.ObservationParametersBrowserModel);
        ReturningSpecimensViewModel = new ParametersBrowserViewModel(model.ReturningSpecimensModel);
        PopulationViewModel = new ParametersBrowserViewModel(model.PopulationModel);
        StatisticsViewModel = new ParametersBrowserViewModel(model.StatisticsModel);
        WriterViewModel = new ParametersBrowserViewModel(model.WriterModel);

        ViewModels =
        [
            DataSourceViewModel,
            ComputedValuesViewModel,
            ObservationsViewModel,
            ReturningSpecimensViewModel,
            PopulationViewModel,
            StatisticsViewModel,
            WriterViewModel
        ];

        SelectedViewModel = DataSourceViewModel;

        model.WriterModel.AddProcess("Zapisz wyniki do pliku", (commandData) => Task.Run(ProcessAndSaveAsync));
    }

    public bool IsLoaded { get; private set; }
    public Action RefreshWindow { get; set; } = () => { };
    public ParametersBrowserViewModel DataSourceViewModel { get; }
    public ParametersBrowserViewModel ComputedValuesViewModel { get; }
    public ParametersBrowserViewModel ObservationsViewModel { get; }
    public ParametersBrowserViewModel PopulationViewModel { get; }
    public ParametersBrowserViewModel ReturningSpecimensViewModel { get; }
    public ParametersBrowserViewModel StatisticsViewModel { get; }
    public ParametersBrowserViewModel WriterViewModel { get; }
    public ObservableCollection<LogEvent> LogEventsItems { get; } = [];
    public ICommand LoadedCommand => new RelayCommand(() => IsLoaded = true);
    public ICommand PreviousCommand => new RelayCommand(Previous, CanGoPrevious);
    public ICommand NextCommand => new RelayCommand(Next, CanGoNext);
    public ICommand BreakCommand => new AsyncRelayCommand(BreakAsync, CanBreak);
    public Visibility BreakCommandVisibility => IsProcessing ? Visibility.Visible : Visibility.Collapsed;
    public ICommand ProcessCommand => new AsyncRelayCommand(ProcessAndSaveAsync, CanProcessAndSave);
    public Visibility ProcessCommandVisibility => IsProcessing ? Visibility.Collapsed : Visibility.Visible;
    public ICommand SaveCurrentConfigCommand => new RelayCommand(SaveCurrentConfig);
    public ICommand LoadNewConfigCommand => new RelayCommand(LoadNewConfig);
    public ICommand ExportCommand => new AsyncRelayCommand(ExportAsync);

    public IList<ObservableObject> ViewModels { get; }

    public ObservableObject? SelectedViewModel
    {
        get => _selectedViewModel;
        set
        {
            _selectedViewModel = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(PreviousCommand));
            OnPropertyChanged(nameof(NextCommand));
            OnPropertyChanged(nameof(ProcessCommand));
        }
    }

    public bool IsProcessing
    {
        get => isProcessing;
        private set
        {
            isProcessing = value;

            if (isProcessing)
            {
                SetMouseCursor(Cursors.Wait);
            }
            else
            {
                SetMouseCursor();
            }

            OnPropertyChanged();
            OnPropertyChanged(nameof(ProcessCommand));
            OnPropertyChanged(nameof(ProcessCommandVisibility));
            OnPropertyChanged(nameof(BreakCommand));
            OnPropertyChanged(nameof(BreakCommandVisibility));
        }
    }

    public ICommand CreateNewConfigCommand => new AsyncRelayCommand(CreateNewConfig);

    private async Task CreateNewConfig()
    {
        var dialog = MessageBox.Show("Czy na pewno chcesz utworzyć nową konfigurację? Niezapisane zmiany w bieżącej konfiguracji zostaną utracone.", "Uwaga", MessageBoxButton.YesNo,
            MessageBoxImage.Warning);
        if (dialog != MessageBoxResult.Yes) return;
        UpdateConfig(new ApplicationParameters());

        SelectedViewModel = null;
        await Task.Delay(100).ConfigureAwait(false);
        RefreshWindow();
        SelectedViewModel = DataSourceViewModel;
    }

    private bool CanProcessAndSave()
    {
        if (IsProcessing) return false;
        return _model.CanRun();
    }

    private static void SetMouseCursor(Cursor? cursor = null)
    {
        WindowHelpers.SetMouseCursor(cursor);
    }

    public void ProcessAndSave()
    {
        ProcessAndSaveAsync().RunSynchronously();
    }

    public async Task ProcessAndSaveAsync()
    {
        if (IsProcessing) return;

        try
        {
            var config = _model.GetConfigFromViewModels();
            var results = config.Results;

            foreach (var result in results.Writers)
            {
                if (result is IWriterParameters parameters)
                {
                    var dialog = FileDialogFactory.GetFileDialogForWriter(parameters);

                    if (dialog.ShowDialog() == true)
                    {
                        parameters.Path = dialog.FileName;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            IsProcessing = true;
            _model.IsProcessed = false;
            await _model.RunAsync().ConfigureAwait(false);
            IsProcessing = false;

            if (IsLoaded && _model.IsProcessed)
            {
                MessageBox.Show("Proces zakończony pomyślnie.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (HirundoException e)
        {
            IsProcessing = false;
            MessageBox.Show($"Wystąpił błąd algorytmu: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception e)
        {
            IsProcessing = false;
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            throw;
        }
    }

    public async Task BreakAsync()
    {
        if (IsProcessing)
        {
            await _model.BreakAsync();
            IsProcessing = false;
        }
    }

    public bool CanBreak()
    {
        return IsProcessing;
    }

    public void UpdateConfig(ApplicationParameters config)
    {
        _model.UpdateConfig(config);
        OnPropertyChanged(nameof(DataSourceViewModel));
        OnPropertyChanged(nameof(ComputedValuesViewModel));
        OnPropertyChanged(nameof(ObservationsViewModel));
        OnPropertyChanged(nameof(ReturningSpecimensViewModel));
        OnPropertyChanged(nameof(PopulationViewModel));
        OnPropertyChanged(nameof(StatisticsViewModel));
        OnPropertyChanged(nameof(WriterViewModel));
        SelectedViewModel = DataSourceViewModel;
    }

    private static string GetFileSourcePathFromUser(IFileSource fileSource)
    {
        var dialog = new OpenFileDialog
        {
            Filter = "Access files (*.mdb)|*.mdb|All files (*.*)|*.*",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Multiselect = false,
            Title = "Wybierz plik bazy danych Access."
        };

        if (dialog.ShowDialog() == true)
        {
            return dialog.FileName;
        }

        return fileSource.Path;
    }

    public ApplicationParameters GetConfig()
    {
        return _model.GetConfigFromViewModels();
    }

    public void Previous()
    {
        if (!CanGoPrevious())
        {
            return;
        }

        if (SelectedViewModel == null) return;
        var index = ViewModels.IndexOf(SelectedViewModel);
        if (index == -1) return;
        SelectedViewModel = ViewModels[index - 1];
    }

    public bool CanGoPrevious()
    {
        return SelectedViewModel != ViewModels.First();
    }

    public void Next()
    {
        if (!CanGoNext())
        {
            return;
        }

        if (SelectedViewModel == null) return;
        var index = ViewModels.IndexOf(SelectedViewModel);
        if (index == -1) return;
        SelectedViewModel = ViewModels[index + 1];
    }

    public bool CanGoNext()
    {
        return SelectedViewModel != ViewModels.Last();
    }

    public void SaveCurrentConfig()
    {
        try
        {
            var config = GetConfig();
            var json = JsonTools.Serialize(config);
            var saveFileDialog = FileDialogFactory.GetFileDialogForFilename("config.json");

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, json);

                var message = $"Konfiguracja została zapisana w {saveFileDialog.FileName}.";

                Log.Information(message);
            }
        }
        catch (Exception e)
        {
            var message = $"Błąd zapisu konfiguracji: {e.Message}";
            Log.Error(message, e);
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            throw;
        }
    }

    public void LoadNewConfig()
    {
        try
        {
            var loadFileDialog = new OpenFileDialog
            {
                Filter = "Pliki JSON (*.json)|*.json",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (loadFileDialog.ShowDialog() == true)
            {
                var json = File.ReadAllText(loadFileDialog.FileName);
                var config = JsonTools.Deserialize<ApplicationParameters>(json);

                UpdateConfig(config);

                var message = $"Konfiguracja została wczytana z {loadFileDialog.FileName}.";
                Log.Information(message);
            }
        }
        catch (Exception e)
        {
            var message = $"Błąd odczytu konfiguracji: {e.Message}";
            Log.Error(message, e);
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            throw;
        }
    }

    public static ApplicationParameters EnsureExistingDataSources(ApplicationParameters config)
    {
        foreach (var database in config.Databases.Databases)
        {
            if (database is IFileSource fileSource)
            {
                if (!File.Exists(fileSource.Path))
                {
                    fileSource.Path = GetFileSourcePathFromUser(fileSource);
                }
            }
        }

        return config;
    }

    public async Task ExportAsync()
    {
        var fileDialog = FileDialogFactory.GetFileDialogForFilename("ringer.xlsx");
        if (fileDialog.ShowDialog() != true) return;
        try
        {
            IsProcessing = true;
            var filename = fileDialog.FileName;
            await _model.ExportAsync(filename);
            Log.Information("Zapisano dane do pliku {filename}.", filename);
            IsProcessing = false;
            MessageBox.Show("Dane zostały zapisane pomyślnie.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        finally
        {
            IsProcessing = false;
        }
    }
}