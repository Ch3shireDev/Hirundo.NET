using Hirundo.Commons;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Computed;

[TypeDescription(
    "Pointedness",
    "Ostrość skrzydła",
    "Wartość ostrości wyliczana na podstawie parametrów skrzydła."
)]
public class PointednessCalculator : WingParametersBase, IComputedValuesCalculator
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

    public override string[] WingParameters { get; set; } = ["D2", "D3", "D4", "D5", "D6", "D7", "D8"];
    public override string WingName { get; set; } = "WING";
    public override string ResultName { get; set; } = "POINTEDNESS";


    public Observation Calculate(Observation observation)
    {
        var pointedness = GetPointedness(observation);

        observation.AddColumn(ResultName, pointedness);
        return observation;
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