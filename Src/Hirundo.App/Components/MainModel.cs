using Hirundo.App.Components.DataSource;
using Hirundo.App.Components.Observations;
using Hirundo.App.Components.Population;
using Hirundo.App.Components.ReturningSpecimens;
using Hirundo.App.Components.Specimens;
using Hirundo.App.Components.Statistics;
using Hirundo.App.Components.Writer;
using Hirundo.Configuration;

namespace Hirundo.App.Components;

public class MainModel(HirundoApp app)
{
    public DataSourceModel DataSourceModel { get; set; } = new();
    public ObservationsModel ObservationsModel { get; set; } = new();
    public PopulationModel PopulationModel { get; set; } = new();
    public ReturningSpecimensModel ReturningSpecimensModel { get; set; } = new();
    public SpecimensModel SpecimensModel { get; set; } = new();
    public StatisticsModel StatisticsModel { get; set; } = new();
    public WriterModel WriterModel { get; set; } = new();

    public void LoadConfig(ApplicationConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        DataSourceModel.DatabaseParameters.Clear();

        foreach (var database in config.Databases)
        {
            DataSourceModel.DatabaseParameters.Add(database);
        }

        ObservationsModel.ObservationsParameters = config.Observations;
        PopulationModel.ConfigPopulation = config.Population;
        ReturningSpecimensModel.ReturningSpecimensParameters = config.ReturningSpecimens;
        SpecimensModel.SpecimensProcessorParameters = config.Specimens;
        StatisticsModel.StatisticsProcessorParameters = config.Statistics;
        WriterModel.SummaryParameters = config.Results;
    }

    private ApplicationConfig GetConfig()
    {
        return new ApplicationConfig
        {
            Databases = DataSourceModel.DatabaseParameters,
            Observations = ObservationsModel.ObservationsParameters,
            Population = PopulationModel.ConfigPopulation,
            ReturningSpecimens = ReturningSpecimensModel.ReturningSpecimensParameters!,
            Specimens = SpecimensModel.SpecimensProcessorParameters ?? throw new InvalidOperationException("Config not loaded"),
            Statistics = StatisticsModel.StatisticsProcessorParameters ?? throw new InvalidOperationException("Config not loaded"),
            Results = WriterModel.SummaryParameters ?? throw new InvalidOperationException("Config not loaded")
        };
    }

    public Task Run()
    {
        var config = GetConfig();
        app.Run(config);
        return Task.CompletedTask;
    }
}