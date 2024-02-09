using Hirundo.Databases;
using Hirundo.Processors.Observations.Conditions;
using Hirundo.Processors.Population;
using Hirundo.Processors.Returning;
using Hirundo.Processors.Specimens;
using Hirundo.Processors.Statistics;
using Hirundo.Writers.Summary;

namespace Hirundo.App;

public class ApplicationConfig
{
    public IList<IDatabaseParameters> Databases { get; init; } = [];
    public ObservationsParameters Observations { get; set; } = new();
    public SpecimensProcessorParameters Specimens { get; set; } = new();
    public ReturningSpecimensParameters ReturningSpecimens { get; set; } = new();
    public PopulationProcessorParameters Population { get; set; } = new();
    public StatisticsProcessorParameters Statistics { get; set; } = new();
    public SummaryParameters Results { get; set; } = new();
}