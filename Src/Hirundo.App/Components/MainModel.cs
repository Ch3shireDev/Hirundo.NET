using Hirundo.Configuration;
using Hirundo.Databases.WPF;
using Hirundo.Processors.Observations.Conditions;
using Hirundo.Processors.Observations.WPF;
using Hirundo.Processors.Population.WPF;
using Hirundo.Processors.Returning.WPF;
using Hirundo.Processors.Specimens.WPF;
using Hirundo.Processors.Statistics.WPF;
using Hirundo.Writers.WPF;
using Serilog;

namespace Hirundo.App.Components;

public class MainModel(HirundoApp app)
{
    public DataSourceModel DataSourceModel { get; set; } = new();
    public ObservationConditionsBrowserModel ObservationConditionsBrowserModel { get; set; } = new();
    public ObservationsParameters ObservationsParameters { get; set; } = new();
    public PopulationModel PopulationModel { get; set; } = new();
    public ReturningSpecimensModel ReturningSpecimensModel { get; set; } = new();
    public SpecimensModel SpecimensModel { get; set; } = new();
    public StatisticsModel StatisticsModel { get; set; } = new();
    public WriterModel WriterModel { get; set; } = new();

    public void SetConfig(ApplicationConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        DataSourceModel.DatabaseParameters.Clear();

        foreach (var database in config.Databases)
        {
            DataSourceModel.DatabaseParameters.Add(database);
        }

        ObservationConditionsBrowserModel.ObservationsParameters = config.Observations;
        PopulationModel.ConfigPopulation = config.Population;
        ReturningSpecimensModel.ReturningSpecimensParameters = config.ReturningSpecimens;
        SpecimensModel.SpecimensProcessorParameters = config.Specimens;
        StatisticsModel.StatisticsProcessorParameters = config.Statistics;
        WriterModel.SummaryParameters = config.Results;
    }

    public ApplicationConfig GetConfig()
    {
        return new ApplicationConfig
        {
            Databases = DataSourceModel.DatabaseParameters,
            Observations = ObservationConditionsBrowserModel.ObservationsParameters,
            Population = PopulationModel.ConfigPopulation,
            ReturningSpecimens = ReturningSpecimensModel.ReturningSpecimensParameters!,
            Specimens = SpecimensModel.SpecimensProcessorParameters ?? throw new InvalidOperationException("Config not loaded"),
            Statistics = StatisticsModel.StatisticsProcessorParameters ?? throw new InvalidOperationException("Config not loaded"),
            Results = WriterModel.SummaryParameters ?? throw new InvalidOperationException("Config not loaded")
        };
    }

    public async Task Run()
    {
        try
        {
            var config = GetConfig();

            app.Run(config);
        }
        catch (Exception e)
        {
            Log.Error($"Błąd działania aplikacji: {e.Message}", e);
            throw;
        }
    }

    public async Task<bool> CanRun()
    {
        if (app == null)
        {
            return false;
        }

        return true;
    }
}