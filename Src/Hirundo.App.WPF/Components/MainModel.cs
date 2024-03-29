using Hirundo.Commons.Repositories.Labels;
using Hirundo.Databases.WPF;
using Hirundo.Processors.Computed.WPF;
using Hirundo.Processors.Observations.WPF;
using Hirundo.Processors.Population.WPF;
using Hirundo.Processors.Returning.WPF;
using Hirundo.Processors.Specimens.WPF;
using Hirundo.Processors.Statistics.WPF;
using Hirundo.Writers.WPF;
using Serilog;

namespace Hirundo.App.WPF.Components;

public class MainModel(
    IHirundoApp app,
    DataSourceModel dataSourceModel,
    ObservationsModel observationParametersBrowserModel,
    PopulationModel populationModel,
    ReturningSpecimensModel returningSpecimensModel,
    SpecimensModel specimensModel,
    StatisticsModel statisticsModel,
    WritersModel writerModel,
    IDataLabelRepository repository,
    ComputedValuesModel computedValuesModel
)
{
    private bool _isProcessing;
    public DataSourceModel DatabasesBrowserModel { get; set; } = dataSourceModel;
    public ObservationsModel ObservationParametersBrowserModel { get; set; } = observationParametersBrowserModel;
    public PopulationModel PopulationModel { get; set; } = populationModel;
    public ReturningSpecimensModel ReturningSpecimensModel { get; set; } = returningSpecimensModel;
    public SpecimensModel SpecimensModel { get; set; } = specimensModel;
    public StatisticsModel StatisticsModel { get; set; } = statisticsModel;
    public WritersModel WriterModel { get; set; } = writerModel;
    public IDataLabelRepository Repository { get; set; } = repository;
    public ComputedValuesModel ComputedValuesModel { get; set; } = computedValuesModel;

    public void UpdateConfig(ApplicationConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        DatabasesBrowserModel.ParametersContainer.Databases.Clear();

        foreach (var database in config.Databases.Databases)
        {
            DatabasesBrowserModel.ParametersContainer.Databases.Add(database);
        }

        DatabasesBrowserModel.UpdateRepository();

        ComputedValuesModel.ParametersContainer = config.ComputedValues;
        ObservationParametersBrowserModel.ParametersContainer = config.Observations;
        PopulationModel.ParametersContainer = config.Population;
        ReturningSpecimensModel.ParametersContainer = config.ReturningSpecimens;
        SpecimensModel.SpecimensProcessorParameters = config.Specimens;
        StatisticsModel.ParametersContainer = config.Statistics;
        WriterModel.ParametersContainer = config.Results;
    }

    public ApplicationConfig GetConfigFromViewModels()
    {
        return new ApplicationConfig
        {
            Databases = DatabasesBrowserModel.ParametersContainer,
            ComputedValues = ComputedValuesModel.ParametersContainer,
            Observations = ObservationParametersBrowserModel.ParametersContainer,
            Population = PopulationModel.ParametersContainer,
            ReturningSpecimens = ReturningSpecimensModel.ParametersContainer!,
            Specimens = SpecimensModel.SpecimensProcessorParameters ?? throw new InvalidOperationException("Config not loaded"),
            Statistics = StatisticsModel.ParametersContainer ?? throw new InvalidOperationException("Config not loaded"),
            Results = WriterModel.ParametersContainer ?? throw new InvalidOperationException("Config not loaded")
        };
    }

    private CancellationTokenSource? _cancellationTokenSource;

    public async Task RunAsync()
    {
        if (_isProcessing)
        {
            return;
        }

        try
        {
            _isProcessing = true;
            var config = GetConfigFromViewModels();
            _cancellationTokenSource = new CancellationTokenSource();
            await Task.Run(() => app.Run(config, _cancellationTokenSource.Token), _cancellationTokenSource.Token);
        }
        catch (OperationCanceledException)
        {
            Log.Information($"Przerwano obliczenia z polecenia użytkownika.");
            return;
        }
        catch (Exception e)
        {
            Log.Error($"Błąd działania aplikacji: {e.Message}", e);
            throw;
        }
        finally
        {
            _isProcessing = false;
        }
    }

    public void Run()
    {
        if (_isProcessing)
        {
            return;
        }

        try
        {
            _isProcessing = true;
            var config = GetConfigFromViewModels();
            app.Run(config);
        }
        catch (OperationCanceledException)
        {
            Log.Information($"Przerwano działanie aplikacji z polecenia użytkownika.");
            return;
        }
        catch (Exception e)
        {
            Log.Error($"Błąd działania aplikacji: {e.Message}", e);
            throw;
        }
        finally
        {
            _isProcessing = false;
        }
    }

    public async Task<bool> CanRunAsync()
    {
        if (_isProcessing)
        {
            return false;
        }

        await Task.Delay(1).ConfigureAwait(false);
        return true;
    }

    public bool CanRun()
    {
        if (_isProcessing)
        {
            return false;
        }

        return true;
    }

    internal async Task BreakAsync()
    {
        if (_cancellationTokenSource is not null)
        {
            _cancellationTokenSource.Cancel();
            await Task.Delay(100).ConfigureAwait(false);
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }
}