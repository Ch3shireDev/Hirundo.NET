using Hirundo.Databases;
using Hirundo.Processors.Computed;
using Hirundo.Processors.Observations;
using Hirundo.Processors.Population;
using Hirundo.Processors.Returning;
using Hirundo.Processors.Specimens;
using Hirundo.Processors.Statistics;
using Hirundo.Writers.Summary;

namespace Hirundo.App;

public class ApplicationParameters
{
    public DatabaseParameters Databases { get; init; } = new();
    public SpecimensParameters Specimens { get; set; } = new();
    public ComputedValuesParameters ComputedValues { get; set; } = new();
    public ObservationsParameters Observations { get; set; } = new();
    public ReturningParameters ReturningSpecimens { get; set; } = new();
    public PopulationParameters Population { get; set; } = new();
    public StatisticsParameters Statistics { get; set; } = new();
    public ResultsParameters Results { get; set; } = new();
}