using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hirundo.App.WPF.Helpers;
using Hirundo.Commons;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Specimens.WPF;
using Hirundo.Writers.Summary;
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
        DataSourceViewModel = new ParametersBrowserViewModel(model.DatabasesBrowserModel);
        ComputedValuesViewModel = new ParametersBrowserViewModel(model.ComputedValuesModel);
        ObservationsViewModel = new ParametersBrowserViewModel(model.ObservationParametersBrowserModel);
        ReturningSpecimensViewModel = new ParametersBrowserViewModel(model.ReturningSpecimensModel);
        PopulationViewModel = new ParametersBrowserViewModel(model.PopulationModel);
        SpecimensViewModel = new SpecimensViewModel(model.SpecimensModel, model.Repository);
        StatisticsViewModel = new ParametersBrowserViewModel(model.StatisticsModel);
        WriterViewModel = new ParametersBrowserViewModel(model.WriterModel);

        ViewModels =
        [
            DataSourceViewModel,
            SpecimensViewModel,
            ComputedValuesViewModel,
            ObservationsViewModel,
            ReturningSpecimensViewModel,
            PopulationViewModel,
            StatisticsViewModel,
            WriterViewModel
        ];

        SelectedViewModel = DataSourceViewModel;

        model.WriterModel.Process = () => Task.Run(ProcessAndSaveAsync);
    }

    public Action RefreshWindow { get; set; } = () => { };
    public ParametersBrowserViewModel DataSourceViewModel { get; }
    public ParametersBrowserViewModel ComputedValuesViewModel { get; }
    public ParametersBrowserViewModel ObservationsViewModel { get; }
    public ParametersBrowserViewModel PopulationViewModel { get; }
    public ParametersBrowserViewModel ReturningSpecimensViewModel { get; }
    public ParametersBrowserViewModel StatisticsViewModel { get; }
    public ParametersBrowserViewModel WriterViewModel { get; }
    public SpecimensViewModel SpecimensViewModel { get; }
    public ObservableCollection<LogEvent> LogEventsItems { get; } = [];
    public ICommand PreviousCommand => new RelayCommand(Previous, CanGoPrevious);
    public ICommand NextCommand => new RelayCommand(Next, CanGoNext);
    public ICommand BreakCommand => new AsyncRelayCommand(BreakAsync, CanBreak);
    public ICommand ProcessAndSaveCommand => new AsyncRelayCommand(ProcessAndSaveAsync, CanProcessAndSave);
    public ICommand SaveCurrentConfigCommand => new RelayCommand(SaveCurrentConfig);
    public ICommand LoadNewConfigCommand => new RelayCommand(LoadNewConfig);

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
            OnPropertyChanged(nameof(ProcessAndSaveCommand));
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
            OnPropertyChanged(nameof(ProcessAndSaveCommand));
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
        try
        {
            Application.Current?.Dispatcher?.Invoke(() => { Mouse.OverrideCursor = cursor; });
        }
        catch (Exception e)
        {
            Log.Error($"Błąd ustawiania kursora. Informacja o błędzie: {e.Message}", e);
            throw;
        }
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
                if (result is CsvSummaryWriterParameters parameters)
                {
                    var dialog = new SaveFileDialog
                    {
                        Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        Title = "Wybierz docelową lokalizację pliku CSV.",
                        FileName = "results.csv"
                    };

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
            OnPropertyChanged(nameof(ProcessAndSaveCommand));
            OnPropertyChanged(nameof(BreakCommand));
            await _model.RunAsync().ConfigureAwait(false);
            IsProcessing = false;
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
        finally
        {
            IsProcessing = false;
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
        OnPropertyChanged(nameof(SpecimensViewModel));
        OnPropertyChanged(nameof(StatisticsViewModel));
        OnPropertyChanged(nameof(WriterViewModel));
        SelectedViewModel = DataSourceViewModel;
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
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,
                FileName = "config.json"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, json);
            }

            var message = $"Konfiguracja została zapisana w {saveFileDialog.FileName}.";

            Log.Information(message);
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
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (loadFileDialog.ShowDialog() == true)
            {
                var json = File.ReadAllText(loadFileDialog.FileName);
                var config = JsonTools.Deserialize(json);
                UpdateConfig(config);
            }

            var message = $"Konfiguracja została wczytana z {loadFileDialog.FileName}.";
            Log.Information(message);
        }
        catch (Exception e)
        {
            var message = $"Błąd odczytu konfiguracji: {e.Message}";
            Log.Error(message, e);
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            throw;
        }
    }
}