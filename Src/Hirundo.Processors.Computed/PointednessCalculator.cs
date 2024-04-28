using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;
using System.Text;

namespace Hirundo.Processors.Computed;

[TypeDescription(
    "Pointedness",
    "Ostrość skrzydła",
    "Wartość ostrości wyliczana na podstawie parametrów skrzydła."
)]
public class PointednessCalculator : WingParametersBase, IComputedValuesCalculator, ISelfExplainer
{
    public PointednessCalculator()
    {
    }

    public PointednessCalculator(string resultName, string[] wingParameters, string wingName)
    {
        ResultName = resultName;
        WingParameters = wingParameters;
        WingName = wingName;
    }

    public sealed override string[] WingParameters { get; set; } = ["D2", "D3", "D4", "D5", "D6", "D7", "D8"];
    public sealed override string WingName { get; set; } = "WING";
    public sealed override string ResultName { get; set; } = "POINTEDNESS";


    public Observation Calculate(Observation observation)
    {
        var pointedness = GetPointedness(observation);

        observation.AddColumn(ResultName, pointedness);
        return observation;
    }

    public string Explain()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Konfiguracja wyliczania ostrości skrzydła (Pointedness):");

        sb.AppendLine($"Wylicza wartości na podstawie parametrów: {string.Join(", ", WingParameters)} oraz {WingName}.");
        sb.AppendLine($"Wynik jest obliczany ze wzoru {ResultName} = ({string.Join(" + ", WingParameters)}) / {WingName}");
        sb.AppendLine($"Wynik jest zapisywany jako parametr: {ResultName}");

        return sb.ToString();
    }

    private decimal? GetPointedness(Observation observation)
    {
        var wingLength = observation.GetDecimal(WingName);

        if (wingLength == null)
        {
            return null;
        }

        if (wingLength == 0)
        {
            return 0;
        }

        var wingParameters = observation.SelectIntValues(WingParameters);

        if (wingParameters.Any(w => w == null))
        {
            return null;
        }

        return wingParameters.Sum() / wingLength.Value;
    }
}