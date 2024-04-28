namespace Hirundo.Commons.Models;

public class ReturningSpecimensResults
{
    public ReturningSpecimensResults()
    {
    }

    public ReturningSpecimensResults(IList<ReturningSpecimenSummary> results, string explanation = "")
    {
        Results = results;
        Explanation = explanation;
    }

    public ReturningSpecimensResults(params ReturningSpecimenSummary[] results)
    {
        Results = results;
    }

    public IList<ReturningSpecimenSummary> Results { get; init; } = [];
    public string Explanation { get; set; } = string.Empty;
}