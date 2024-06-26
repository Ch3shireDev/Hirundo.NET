﻿using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;
using System.Text;

namespace Hirundo.Processors.Computed;

/// <summary>
///     Kalkulator symetrii, opisywany jako suma wartości na prawo minus suma wartości na lewo od pierwszego znalezionego
///     zera w parametrach skrzydła, podzielona przez sumę wszystkich wartości.
/// </summary>
[TypeDescription(
    "Symmetry",
    "Skośność skrzydła",
    "Wartość skośności wyliczana na podstawie parametrów skrzydła."
)]
public class SymmetryCalculator : WingParametersBase, IComputedValuesCalculator, ISelfExplainer
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

    public sealed override string[] WingParameters { get; set; } = ["D2", "D3", "D4", "D5", "D6", "D7", "D8"];
    public sealed override string WingName { get; set; } = "WING";
    public sealed override string ResultName { get; set; } = "SYMMETRY";

    public Observation Calculate(Observation observation)
    {
        observation.AddColumn(ResultName, CalculateSymmetry(observation));

        return observation;
    }

    public string Explain()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Konfiguracja wyliczania symetrii skrzydła (Symmetry):");

        sb.AppendLine($"Wylicza wartości na podstawie parametrów: {string.Join(", ", WingParameters)} oraz {WingName}.");
        sb.AppendLine($"Wynik jest obliczany metodą: od sumy po prawej stronie pierwszego zera odejmowana jest suma po lewej stronie pierwszego zera, i dzielona przez {WingName}");
        sb.AppendLine($"Wynik jest zapisywany jako parametr: {ResultName}");

        return sb.ToString();
    }

    private decimal? CalculateSymmetry(Observation observation)
    {
        var wingValues = observation.SelectIntValues(WingParameters);

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