using Hirundo.Commons.Helpers;
using Hirundo.Processors.Observations;
using System.Text;

namespace Hirundo.App.Explainers.Observations;
public class ObservationsExplainer : ParametersExplainer<ObservationsParameters>
{
    public override string Explain(ObservationsParameters parameters)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Konfiguracja obserwacji:");
        sb.AppendLine($"Obserwacje (czyli wpisy w bazie danych) są filtrowane na podstawie {parameters.Conditions.Count} warunków.");

        foreach (var condition in parameters.Conditions)
        {
            sb.AppendLine(ExplainerHelpers.Explain(condition));
        }

        sb.AppendLine();

        return sb.ToString();
    }
}

public class IsEqualConditionExplainer : ParametersExplainer<IsEqualCondition>
{
    public override string Explain(IsEqualCondition parameters)
    {
        return $"Wartość {parameters.ValueName} musi być równa {parameters.Value}";
    }
}

public class IsGreaterThanConditionExplainer : ParametersExplainer<IsGreaterThanCondition>
{
    public override string Explain(IsGreaterThanCondition parameters)
    {
        return $"Wartość {parameters.ValueName} musi być większa niż {parameters.Value}";
    }
}

public class IsLowerThanConditionExplainer : ParametersExplainer<IsLowerThanCondition>
{
    public override string Explain(IsLowerThanCondition parameters)
    {
        return $"Wartość {parameters.ValueName} musi być mniejsza niż {parameters.Value}";
    }
}

public class IsGreaterOrEqualExplainer : ParametersExplainer<IsGreaterOrEqualCondition>
{
    public override string Explain(IsGreaterOrEqualCondition parameters)
    {
        return $"Wartość {parameters.ValueName} musi być większa lub równa {parameters.Value}";
    }
}

public class IsLowerOrEqualExplainer : ParametersExplainer<IsLowerOrEqualCondition>
{
    public override string Explain(IsLowerOrEqualCondition parameters)
    {
        return $"Wartość {parameters.ValueName} musi być mniejsza lub równa {parameters.Value}";
    }
}

public class IsInSeasonExplainer : ParametersExplainer<IsInSeasonCondition>
{
    public override string Explain(IsInSeasonCondition parameters)
    {
        return $"Wartość {parameters.DateColumnName} musi być w sezonie - w zakresie od {parameters.Season.StartMonth:D2}.{parameters.Season.StartDay:D2} do {parameters.Season.EndMonth:D2}.{parameters.Season.EndDay:D2}, dowolnego roku.";
    }
}

public class IsInSetExplainer : ParametersExplainer<IsInSetCondition>
{
    public override string Explain(IsInSetCondition parameters)
    {
        return $"Wartość {parameters.ValueName} musi być jedną z wartości: {string.Join(", ", parameters.Values)}";
    }
}

public class IsInTimeBlockExplainer : ParametersExplainer<IsInTimeBlockCondition>
{
    public override string Explain(IsInTimeBlockCondition parameters)
    {
        var acceptType = parameters.RejectNullValues ? "odrzucane" : "zatwierdzane";

        return $"Wartość {parameters.ValueName} musi być w przedziale godzin od {parameters.TimeBlock.StartHour} do {parameters.TimeBlock.EndHour}. Puste wartości {parameters.ValueName} są {acceptType}.";
    }
}