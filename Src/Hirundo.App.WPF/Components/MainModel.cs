using Hirundo.Commons.Repositories;
using Hirundo.Databases.WPF;
using Hirundo.Processors.Computed.WPF;
using Hirundo.Processors.Observations.WPF;
using Hirundo.Processors.Population.WPF;
using Hirundo.Processors.Returning.WPF;
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
    StatisticsModel statisticsModel,
    WritersModel writerModel,
    ILabelsRepository labelsRepository,
    ComputedValuesModel computedValuesModel
)
{
    private CancellationTokenSource? _cancellationTokenSource;
    private bool _isProcessing;
    public DataSourceModel DatabasesBrowserModel { get; set; } = dataSourceModel;
    public ObservationsModel ObservationParametersBrowserModel { get; set; } = observationParametersBrowserModel;
    public PopulationModel PopulationModel { get; set; } = populationModel;
    public ReturningSpecimensModel ReturningSpecimensModel { get; set; } = returningSpecimensModel;
    public StatisticsModel StatisticsModel { get; set; } = statisticsModel;
    public WritersModel WriterModel { get; set; } = writerModel;
    public ILabelsRepository Repository { get; set; } = labelsRepository;
    public ComputedValuesModel ComputedValuesModel { get; set; } = computedValuesModel;
    public bool IsProcessed { get; internal set; }

    public void UpdateConfig(ApplicationParameters config)
    {
        ArgumentNullException.ThrowIfNull(config);

        DatabasesBrowserModel.Parameters.Clear();

        foreach (var database in config.Databases.Databases)
        {
            DatabasesBrowserModel.Parameters.Add(database);
        }

        DatabasesBrowserModel.UpdateRepository();

        ComputedValuesModel.ParametersContainer = config.ComputedValues;
        ObservationParametersBrowserModel.ParametersContainer = config.Observations;
        PopulationModel.ParametersContainer = config.Population;
        ReturningSpecimensModel.ParametersContainer = config.ReturningSpecimens;
        StatisticsModel.ParametersContainer = config.Statistics;
        WriterModel.ParametersContainer = config.Results;
    }

    public ApplicationParameters GetConfigFromViewModels()
    {
        return new ApplicationParameters
        {
            Databases = DatabasesBrowserModel.ParametersContainer,
            ComputedValues = ComputedValuesModel.ParametersContainer,
            Observations = ObservationParametersBrowserModel.ParametersContainer,
            Population = PopulationModel.ParametersContainer,
            ReturningSpecimens = ReturningSpecimensModel.ParametersContainer,
            Statistics = StatisticsModel.ParametersContainer,
            Results = WriterModel.ParametersContainer
        };
    }

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
            IsProcessed = true;
        }
        catch (OperationCanceledException)
        {
            Log.Information("Przerwano obliczenia z polecenia użytkownika.");
            IsProcessed = false;
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
            Log.Information("Przerwano działanie aplikacji z polecenia użytkownika.");
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