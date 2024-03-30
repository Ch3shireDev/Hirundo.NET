using Hirundo.Commons.Helpers;
using Hirundo.Processors.Specimens;
using System.Text;

namespace Hirundo.App.Explainers.Specimens;
public class SpecimensExplainer : ParametersExplainer<SpecimensParameters>
{
    public override string Explain(SpecimensParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));

        var sb = new StringBuilder();
        sb.AppendLine("Konfiguracja osobników:");
        sb.AppendLine($"Obserwacje są grupowane w osobniki po wartości: {parameters.SpecimenIdentifier}");
        sb.AppendLine();
        return sb.ToString();
    }
}
