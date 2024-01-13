using Hirundo.Databases;
using Hirundo.Filters.Observations;
using Hirundo.Filters.Specimens;
using Hirundo.Processors.Population;
using Hirundo.Processors.Specimens;
using Hirundo.Processors.Statistics;
using Hirundo.Writers.Summary;

namespace Hirundo.Configuration;

public class ApplicationConfig
{
    public IList<IDatabaseParameters> Databases { get; init; } = [];
    public ObservationsParameters Observations { get; set; } = null!;
    public SpecimensProcessorParameters Specimens { get; set; } = null!;
    public ReturningSpecimensParameters ReturningSpecimens { get; set; } = null!;
    public PopulationProcessorParameters Population { get; set; } = null!;
    public StatisticsProcessorParameters Statistics { get; set; } = null!;
    public SummaryParameters Results { get; set; } = null!;
}