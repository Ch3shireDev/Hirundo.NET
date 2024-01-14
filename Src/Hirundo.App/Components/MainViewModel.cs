using System.Collections.ObjectModel;
using System.Windows.Input;
using Hirundo.App.Components.DataSource;
using Hirundo.App.Components.Observations;
using Hirundo.App.Components.Population;
using Hirundo.App.Components.ReturningSpecimens;
using Hirundo.App.Components.Specimens;
using Hirundo.App.Components.Statistics;
using Hirundo.App.Components.Writer;
using Serilog.Events;

namespace Hirundo.App.Components;

internal sealed class MainViewModel(MainModel model) : ViewModelBase
{
    public DataSourceViewModel DataSourceViewModel { get; } = new(model.DataSourceModel);
    public ObservationsViewModel ObservationsViewModel { get; } = new(model.ObservationsModel);
    public ReturningSpecimensViewModel ReturningSpecimensViewModel { get; } = new(model.ReturningSpecimensModel);
    public PopulationViewModel PopulationViewModel { get; } = new(model.PopulationModel);
    public SpecimensViewModel SpecimensViewModel { get; } = new(model.SpecimensModel);
    public StatisticsViewModel StatisticsViewModel { get; } = new(model.StatisticsModel);
    public WriterViewModel WriterViewModel { get; } = new(model.WriterModel, model.Run);

    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ObservableCollection<LogEvent> Items { get; } = [];
    public ICommand CancelCommand { get; } = null!;
    public ICommand PreviousCommand { get; } = null!;
    public ICommand NextCommand { get; } = null!;
}