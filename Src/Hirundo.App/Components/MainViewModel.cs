using System.Collections.ObjectModel;
using System.Windows.Input;
using Hirundo.Commons.WPF;
using Hirundo.Configuration;
using Hirundo.Databases.WPF;
using Hirundo.Processors.Observations.WPF;
using Hirundo.Processors.Population.WPF;
using Hirundo.Processors.Returning.WPF;
using Hirundo.Processors.Specimens.WPF;
using Hirundo.Processors.Statistics.WPF;
using Hirundo.Writers.WPF;
using Serilog.Events;

namespace Hirundo.App.Components;

public sealed class MainViewModel(MainModel model) : ViewModelBase
{
    public DataSourceViewModel DataSourceViewModel { get; } = new(model.DataSourceModel);
    public ObservationsViewModel ObservationsViewModel { get; } = new(model.ObservationsModel);
    public ReturningSpecimensViewModel ReturningSpecimensViewModel { get; } = new(model.ReturningSpecimensModel);
    public PopulationViewModel PopulationViewModel { get; } = new(model.PopulationModel);
    public SpecimensViewModel SpecimensViewModel { get; } = new(model.SpecimensModel);
    public StatisticsViewModel StatisticsViewModel { get; } = new(model.StatisticsModel);
    public WriterViewModel WriterViewModel { get; } = new(model.WriterModel, model.Run);
    public ObservableCollection<LogEvent> Items { get; } = [];
    public ICommand CancelCommand { get; } = null!;
    public ICommand PreviousCommand { get; } = null!;
    public ICommand NextCommand { get; } = null!;
    public void SetConfig(ApplicationConfig config)
    {
        model.SetConfig(config);
    }

    public ApplicationConfig GetConfig()
    {
        return model.GetConfig();
    }
}