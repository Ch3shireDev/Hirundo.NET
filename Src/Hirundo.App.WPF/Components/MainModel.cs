using Hirundo.Commons.Repositories.Labels;
using Hirundo.Databases.WPF;
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
    ObservationParametersBrowserModel observationParametersBrowserModel,
    PopulationModel populationModel,
    ReturningSpecimensModel returningSpecimensModel,
    SpecimensModel specimensModel,
    StatisticsModel statisticsModel,
    WriterModel writerModel,
    IDataLabelRepository repository
)
{
    private bool _isProcessing;
    public DataSourceModel DataSourceModel { get; set; } = dataSourceModel;
    public ObservationParametersBrowserModel ObservationParametersBrowserModel { get; set; } = observationParametersBrowserModel;
    public PopulationModel PopulationModel { get; set; } = populationModel;
    public ReturningSpecimensModel ReturningSpecimensModel { get; set; } = returningSpecimensModel;
    public SpecimensModel SpecimensModel { get; set; } = specimensModel;
    public StatisticsModel StatisticsModel { get; set; } = statisticsModel;
    public WriterModel WriterModel { get; set; } = writerModel;
    public IDataLabelRepository Repository { get; set; } = repository;

    public void UpdateConfig(ApplicationConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        DataSourceModel.DatabaseParameters.Clear();

        foreach (var database in config.Databases)
        {
            DataSourceModel.DatabaseParameters.Add(database);
        }

        DataSourceModel.UpdateRepository();

        ObservationParametersBrowserModel.ObservationsParameters = config.Observations;
        PopulationModel.ConfigPopulation = config.Population;
        ReturningSpecimensModel.ReturningSpecimensParameters = config.ReturningSpecimens;
        SpecimensModel.SpecimensProcessorParameters = config.Specimens;
        StatisticsModel.StatisticsProcessorParameters = config.Statistics;
        WriterModel.SummaryParameters = config.Results;
    }

    public ApplicationConfig GetConfigFromViewModels()
    {
        return new ApplicationConfig
        {
            Databases = DataSourceModel.DatabaseParameters,
            Observations = ObservationParametersBrowserModel.ObservationsParameters,
            Population = PopulationModel.ConfigPopulation,
            ReturningSpecimens = ReturningSpecimensModel.ReturningSpecimensParameters!,
            Specimens = SpecimensModel.SpecimensProcessorParameters ?? throw new InvalidOperationException("Config not loaded"),
            Statistics = StatisticsModel.StatisticsProcessorParameters ?? throw new InvalidOperationException("Config not loaded"),
            Results = WriterModel.SummaryParameters ?? throw new InvalidOperationException("Config not loaded")
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
            await Task.Run(() => app.Run(config));
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
}