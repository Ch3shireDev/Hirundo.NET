using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Hirundo.App.Helpers;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Configuration;
using Hirundo.Processors.Specimens.WPF;
using Hirundo.Writers.WPF;
using Microsoft.Win32;
using Serilog;
using Serilog.Events;

namespace Hirundo.App.Components;

public sealed class MainViewModel : ViewModelBase
{
    private readonly MainModel _model;
    private ViewModelBase? _selectedViewModel;

    private bool isProcessing;

    public MainViewModel(MainModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        _model = model;
        DataSourceViewModel = new(model.DataSourceModel);
        ParametersBrowserViewModel = new(model.ObservationParametersBrowserModel);
        ReturningSpecimensViewModel = new(model.ReturningSpecimensModel);
        PopulationViewModel = new(model.PopulationModel);
        SpecimensViewModel = new(model.SpecimensModel);
        StatisticsViewModel = new(model.StatisticsModel);
        WriterViewModel = new(model.WriterModel, model.Run);

        ViewModels = new List<ViewModelBase>
        {
            DataSourceViewModel,
            ParametersBrowserViewModel,
            ReturningSpecimensViewModel,
            PopulationViewModel,
            SpecimensViewModel,
            StatisticsViewModel,
            WriterViewModel
        };

        SelectedViewModel = DataSourceViewModel;
    }

    public Action RefreshWindow { get; set; } = () => { };
    public ParametersBrowserViewModel DataSourceViewModel { get; }
    public ParametersBrowserViewModel ParametersBrowserViewModel { get; }
    public ParametersBrowserViewModel PopulationViewModel { get; }
    public ParametersBrowserViewModel ReturningSpecimensViewModel { get; }
    public ParametersBrowserViewModel StatisticsViewModel { get; }
    public SpecimensViewModel SpecimensViewModel { get; }
    public WriterViewModel WriterViewModel { get; }
    public ObservableCollection<LogEvent> LogEventsItems { get; } = [];
    public ICommand PreviousCommand => new RelayCommand(Previous, CanGoPrevious);
    public ICommand NextCommand => new RelayCommand(Next, CanGoNext);
    public ICommand ProcessAndSaveCommand => new AsyncRelayCommand(ProcessAndSave, CanProcessAndSave);
    public ICommand SaveCurrentConfigCommand => new RelayCommand(SaveCurrentConfig);
    public ICommand LoadNewConfigCommand => new RelayCommand(LoadNewConfig);

    public IList<ViewModelBase> ViewModels { get; }

    public ViewModelBase? SelectedViewModel
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

    private bool IsProcessing
    {
        get => isProcessing;
        set
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
        SetConfig(new ApplicationConfig());

        SelectedViewModel = null;
        RefreshWindow();
        SelectedViewModel = DataSourceViewModel;
    }

    private Task<bool> CanProcessAndSave()
    {
        if (IsProcessing) return Task.FromResult(false);
        return _model.CanRun();
    }

    private static void SetMouseCursor(Cursor? cursor = null)
    {
        try
        {
            Application.Current.Dispatcher.Invoke(() => { Mouse.OverrideCursor = cursor; });
        }
        catch (Exception e)
        {
            Log.Error($"Błąd ustawiania kursora. Informacja o błędzie: {e.Message}", e);
            throw;
        }
    }

    private async Task ProcessAndSave()
    {
        if (IsProcessing) return;

        try
        {
            IsProcessing = true;
            await Task.Run(_model.Run);
            IsProcessing = false;
        }
        catch (Exception e)
        {
            IsProcessing = false;
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsProcessing = false;
        }
    }

    public void SetConfig(ApplicationConfig config)
    {
        _model.SetConfig(config);
        OnPropertyChanged(nameof(DataSourceViewModel));
        OnPropertyChanged(nameof(ParametersBrowserViewModel));
        OnPropertyChanged(nameof(ReturningSpecimensViewModel));
        OnPropertyChanged(nameof(PopulationViewModel));
        OnPropertyChanged(nameof(SpecimensViewModel));
        OnPropertyChanged(nameof(StatisticsViewModel));
        OnPropertyChanged(nameof(WriterViewModel));
        SelectedViewModel = DataSourceViewModel;
    }

    public ApplicationConfig GetConfig()
    {
        return _model.GetConfig();
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
                SetConfig(config);
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