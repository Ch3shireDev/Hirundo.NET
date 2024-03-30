using Hirundo.Commons.Helpers;
using Hirundo.Processors.Computed;
using System.Text;

namespace Hirundo.App.Explainers.Computed;

public class PointednessExplainer : ParametersExplainer<PointednessCalculator>
{
    public override string Explain(PointednessCalculator parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));

        var sb = new StringBuilder();
        sb.AppendLine("Konfiguracja wyliczania ostrości skrzydła (Pointedness):");

        sb.AppendLine($"Wylicza wartości na podstawie parametrów: {string.Join(", ", parameters.WingParameters)} oraz {parameters.WingName}.");
        sb.AppendLine($"Wynik jest obliczany ze wzoru {parameters.ResultName} = ({string.Join(" + ", parameters.WingParameters)}) / {parameters.WingName}");
        sb.AppendLine($"Wynik jest zapisywany jako parametr: {parameters.ResultName}");

        return sb.ToString();
    }
}

public class SymmetryExplainer : ParametersExplainer<SymmetryCalculator>
{
    public override string Explain(SymmetryCalculator parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));

        var sb = new StringBuilder();
        sb.AppendLine("Konfiguracja wyliczania symetrii skrzydła (Symmetry):");

        sb.AppendLine($"Wylicza wartości na podstawie parametrów: {string.Join(", ", parameters.WingParameters)} oraz {parameters.WingName}.");
        sb.AppendLine($"Wynik jest obliczany metodą: od sumy po prawej stronie pierwszego zera odejmowana jest suma po lewej stronie pierwszego zera, i dzielona przez {parameters.WingName}");
        sb.AppendLine($"Wynik jest zapisywany jako parametr: {parameters.ResultName}");

        return sb.ToString();
    }
}
