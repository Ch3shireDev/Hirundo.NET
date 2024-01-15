﻿using Hirundo.Configuration;
using Hirundo.Databases.WPF;
using Hirundo.Processors.Observations.WPF;
using Hirundo.Processors.Population.WPF;
using Hirundo.Processors.Returning.WPF;
using Hirundo.Processors.Specimens.WPF;
using Hirundo.Processors.Statistics.WPF;
using Hirundo.Writers.WPF;

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