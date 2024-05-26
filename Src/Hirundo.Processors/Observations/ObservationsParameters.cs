using Hirundo.Commons.Helpers;
using System.Text;

namespace Hirundo.Processors.Observations;

public class ObservationsParameters : ISelfExplainer
{
    public IList<IObservationCondition> Conditions { get; init; } = [];

    public string Explain()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Konfiguracja obserwacji:");
        sb.AppendLine($"Obserwacje (czyli wpisy w bazie danych) są filtrowane na podstawie {Conditions.Count} warunków.");

        foreach (var condition in Conditions)
        {
            sb.AppendLine(ExplainerHelpers.Explain(condition));
        }

        sb.AppendLine();

        return sb.ToString();
    }
}