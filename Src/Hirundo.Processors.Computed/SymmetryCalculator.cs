using Hirundo.Commons;

namespace Hirundo.Processors.Computed;

public class WingParametersBase
{
    public virtual string[] WingParameters { get; set; } = ["D2", "D3", "D4", "D5", "D6", "D7", "D8"];
    public virtual string WingName { get; set; } = "WING";
    public virtual string ResultName { get; set; } = string.Empty;
}

/// <summary>
///     Kalkulator symetrii, opisywany jako suma wartości na prawo minus suma wartości na lewo od pierwszego znalezionego
///     zera w parametrach skrzydła, podzielona przez sumę wszystkich wartości.
/// </summary>
[TypeDescription("Symmetry")]
public class SymmetryCalculator : WingParametersBase, IComputedValuesCalculator
{
    /// <summary>
    ///     Kalkulator symetrii, opisywany jako suma wartości na prawo minus suma wartości na lewo od pierwszego znalezionego
    ///     zera w parametrach skrzydła, podzielona przez sumę wszystkich wartości.
    /// </summary>
    /// <param name="resultName">Nazwa parametru wynikowego określającego symetrię skrzydła.</param>
    /// <param name="wingParameters">Nazwy siedmiu parametrów z formuły skrzydła.</param>
    /// <param name="wingName">Nazwa parametru określającego długość skrzydła.</param>
    public SymmetryCalculator(string resultName, string[] wingParameters, string wingName)
    {
        ResultName = resultName;
        WingParameters = wingParameters;
        WingName = wingName;
    }

    public SymmetryCalculator()
    {
    }

    public override string[] WingParameters { get; set; } = ["D2", "D3", "D4", "D5", "D6", "D7", "D8"];
    public override string WingName { get; set; } = "WING";
    public override string ResultName { get; set; } = "SYMMETRY";

    public Observation Calculate(Observation observation)
    {
        observation.AddColumn(ResultName, CalculateSymmetry(observation));

        return observation;
    }

    private decimal? CalculateSymmetry(Observation observation)
    {
        var wingValues = observation.GetIntValues(WingParameters);

        if (wingValues.Any(x => x is null))
        {
            return null;
        }

        var wingLength = observation.GetDecimal(WingName);

        switch (wingLength)
        {
            case null:
            case 0m:
                return null;
        }

        var zeroIndex = Array.IndexOf(wingValues, 0);

        if (zeroIndex == -1)
        {
            return null;
        }

        var leftSum = wingValues.Take(zeroIndex).Sum();
        var rightSum = wingValues.Skip(zeroIndex).Sum();

        var result = (rightSum - leftSum) / wingLength;

        return result;
    }
}