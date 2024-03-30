using Hirundo.Commons.Helpers;
using Hirundo.Processors.Computed;
using System.Text;

namespace Hirundo.App.Explainers.Computed;
public class ComputedExplainer : ParametersExplainer<ComputedValuesParameters>
{
    public override string Explain(ComputedValuesParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));

        var sb = new StringBuilder();
        sb.AppendLine("Konfiguracja wyliczanych wartości:");
        sb.AppendLine($"Liczba dodatkowych wartości: {parameters.ComputedValues.Count}");
        sb.AppendLine();
        foreach (var computedValue in parameters.ComputedValues)
        {
            sb.AppendLine(ExplainerHelpers.Explain(computedValue));
        }

        return sb.ToString();
    }
}
