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
    WriterModel writerModel
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

    public ApplicationConfig CreateConfig()
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

    public async Task Run()
    {
        if (_isProcessing)
        {
            return;
        }

        try
        {
            _isProcessing = true;
            var config = CreateConfig();
            var task = new Task(() => app.Run(config));
            await task.ConfigureAwait(false);
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

    public async Task<bool> CanRun()
    {
        if (_isProcessing)
        {
            return false;
        }

        await Task.Delay(1).ConfigureAwait(false);
        return true;
    }
}