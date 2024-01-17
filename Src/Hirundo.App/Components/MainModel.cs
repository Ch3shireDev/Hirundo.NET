using System.IO;
using Hirundo.Configuration;
using Hirundo.Databases.WPF;
using Hirundo.Processors.Observations.WPF;
using Hirundo.Processors.Population.WPF;
using Hirundo.Processors.Returning.WPF;
using Hirundo.Processors.Specimens.WPF;
using Hirundo.Processors.Statistics.WPF;
using Hirundo.Serialization.Json;
using Hirundo.Writers.WPF;
using Newtonsoft.Json;
using Serilog;

namespace Hirundo.App.Components;

public class MainModel
{
    private readonly HirundoApp _app;

    public MainModel(HirundoApp app)
    {
        _app = app;
    }

    public DataSourceModel DataSourceModel { get; set; } = new();
    public ObservationsModel ObservationsModel { get; set; } = new();
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

        ObservationsModel.ObservationsParameters = config.Observations;
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
            Observations = ObservationsModel.ObservationsParameters,
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

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.Indented,
                Converters = { new HirundoJsonConverter() }
            };

            await File.WriteAllTextAsync($"{DateTime.Now.Ticks}.json", JsonConvert.SerializeObject(config, Formatting.Indented, settings));

            _app.Run(config);
        }
        catch (Exception e)
        {
            Log.Error($"Błąd działania aplikacji: {e.Message}", e);
            throw;
        }
    }

    public async Task<bool> CanRun()
    {
        if (_app == null)
        {
            return false;
        }

        return true;
    }
}