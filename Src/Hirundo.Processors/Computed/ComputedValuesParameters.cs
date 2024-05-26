using Hirundo.Commons.Helpers;
using System.Text;

namespace Hirundo.Processors.Computed;

public class ComputedValuesParameters : ISelfExplainer
{
    public IList<IComputedValuesCalculator> ComputedValues { get; set; } = [];

    public string Explain()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Konfiguracja wyliczanych wartości:");
        sb.AppendLine($"Liczba dodatkowych wartości: {ComputedValues.Count}");
        sb.AppendLine();

        foreach (var computedValue in ComputedValues)
        {
            sb.AppendLine(ExplainerHelpers.Explain(computedValue));
        }

        return sb.ToString();
    }
}