using Hirundo.Commons.Helpers;
using System.Text;

namespace Hirundo.App.Explainers;

public class ApplicationParametersExplainer : ParametersExplainer<ApplicationParameters>
{
    public override string Explain(ApplicationParameters config)
    {
        var sb = new StringBuilder();
        sb.AppendLine(ExplainerHelpers.Explain(config.Databases));
        sb.AppendLine(ExplainerHelpers.Explain(config.Specimens));
        sb.AppendLine(ExplainerHelpers.Explain(config.ComputedValues));
        sb.AppendLine(ExplainerHelpers.Explain(config.Observations));
        sb.AppendLine(ExplainerHelpers.Explain(config.ReturningSpecimens));
        sb.AppendLine(ExplainerHelpers.Explain(config.Population));
        sb.AppendLine(ExplainerHelpers.Explain(config.Statistics));
        sb.AppendLine(ExplainerHelpers.Explain(config.Results));
        return sb.ToString();
    }
}
