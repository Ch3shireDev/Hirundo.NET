using Hirundo.Commons.Helpers;
using Hirundo.Databases;
using Hirundo.Processors.Computed;
using Hirundo.Processors.Observations;
using Hirundo.Processors.Population;
using Hirundo.Processors.Returning;
using Hirundo.Processors.Statistics;
using Hirundo.Writers;
using System.Text;

namespace Hirundo.App;

public class ApplicationParameters : ISelfExplainer
{
    public DatabaseParameters Databases { get; init; } = new();
    public ComputedValuesParameters ComputedValues { get; set; } = new();
    public ObservationsParameters Observations { get; set; } = new();
    public ReturningParameters ReturningSpecimens { get; set; } = new();
    public PopulationParameters Population { get; set; } = new();
    public StatisticsParameters Statistics { get; set; } = new();
    public ResultsParameters Results { get; set; } = new();

    public string Explain()
    {
        var sb = new StringBuilder();
        sb.AppendLine(ExplainerHelpers.Explain(Databases));
        sb.AppendLine(ExplainerHelpers.Explain(ComputedValues));
        sb.AppendLine(ExplainerHelpers.Explain(Observations));
        sb.AppendLine(ExplainerHelpers.Explain(ReturningSpecimens));
        sb.AppendLine(ExplainerHelpers.Explain(Population));
        sb.AppendLine(ExplainerHelpers.Explain(Statistics));
        sb.AppendLine(ExplainerHelpers.Explain(Results));
        return sb.ToString();
    }
}