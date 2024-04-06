using Hirundo.Commons.Helpers;
using Hirundo.Processors.Returning;
using Hirundo.Processors.Returning.Conditions;
using System.Text;

namespace Hirundo.App.Explainers.Returning;
public class ReturningExplainer : ParametersExplainer<ReturningParameters>
{
    public override string Explain(ReturningParameters parameters)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Konfiguracja powracających osobników:");
        sb.AppendLine($"Określanie powracających osobników jest przeprowadzane na podstawie {parameters.Conditions.Count} warunków.");
        sb.AppendLine("Warunki:");
        foreach (var condition in parameters.Conditions)
        {
            sb.AppendLine(ExplainerHelpers.Explain(condition));
        }
        sb.AppendLine();
        return sb.ToString();
    }
}

public class AlternativeReturningExplainer : ParametersExplainer<AlternativeReturningCondition>
{
    public override string Explain(AlternativeReturningCondition parameters)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Warunki alternatywne (jeden lub drugi lub obydwa:");
        foreach (var condition in parameters.Conditions)
        {
            var results = ExplainerHelpers.Explain(condition).Split('\n');
            foreach (var result in results)
            {
                sb.AppendLine("    " + result + "\n");
            }
        }

        return sb.ToString();
    }
}

public class IsEqualReturningExplainer : ParametersExplainer<IsEqualReturningCondition>
{
    public override string Explain(IsEqualReturningCondition parameters)
    {
        return $"Którakolwiek z obserwacji osobnika musi mieć wartość {parameters.ValueName} równą {parameters.Value}.";
    }
}

public class IsGreaterOrEqualReturningExplainer : ParametersExplainer<IsGreaterOrEqualReturningCondition>
{
    public override string Explain(IsGreaterOrEqualReturningCondition parameters)
    {
        return $"Którakolwiek z obserwacji osobnika musi mieć wartość {parameters.ValueName} większą lub równą {parameters.Value}.";
    }
}

public class IsGreaterThanReturningExplainer : ParametersExplainer<IsGreaterThanReturningCondition>
{
    public override string Explain(IsGreaterThanReturningCondition parameters)
    {
        return $"Którakolwiek z obserwacji osobnika musi mieć wartość {parameters.ValueName} większą niż {parameters.Value}.";
    }
}

public class IsInSetReturningExplainer : ParametersExplainer<IsInSetReturningCondition>
{
    public override string Explain(IsInSetReturningCondition parameters)
    {
        return $"Którakolwiek z obserwacji osobnika musi mieć wartość {parameters.ValueName} z zestawu: {string.Join(", ", parameters.Values)}";
    }
}

public class IsLowerOrEqualReturningExplainer : ParametersExplainer<IsLowerOrEqualReturningCondition>
{
    public override string Explain(IsLowerOrEqualReturningCondition parameters)
    {
        return $"Którakolwiek z obserwacji osobnika musi mieć wartość {parameters.ValueName} mniejszą lub równą {parameters.Value}.";
    }
}

public class IsLowerThanReturningExplainer : ParametersExplainer<IsLowerThanReturningCondition>
{
    public override string Explain(IsLowerThanReturningCondition parameters)
    {
        return $"Którakolwiek z obserwacji osobnika musi mieć wartość {parameters.ValueName} mniejszą niż {parameters.Value}.";
    }
}

public class IsNotEqualReturningExplainer : ParametersExplainer<IsNotEqualReturningCondition>
{
    public override string Explain(IsNotEqualReturningCondition parameters)
    {
        return $"Którakolwiek z obserwacji osobnika musi mieć wartość {parameters.ValueName} różną od {parameters.Value}.";
    }
}

public class ReturnsAfterTimePeriodExplainer : ParametersExplainer<ReturnsAfterTimePeriodCondition>
{
    public override string Explain(ReturnsAfterTimePeriodCondition parameters)
    {
        return $"Osobnik powraca po upływie {parameters.TimePeriodInDays} od ostatniego wystąpienia - istnieje odstęp dat większy lub równy niż {parameters.TimePeriodInDays} dni.";
    }
}

public class ReturnsNotEarlierThanGivenDateExplainer : ParametersExplainer<ReturnsNotEarlierThanGivenDateNextYearCondition>
{
    public override string Explain(ReturnsNotEarlierThanGivenDateNextYearCondition parameters)
    {
        return $"Osobnik powraca nie wcześniej niż w {parameters.Month:D2}.{parameters.Day:D2} następnego roku, obliczane na podstawie daty.";
    }
}