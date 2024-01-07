using Hirundo.Commons;

namespace Hirundo.Filters.Population;

/// <summary>
///     Filtr ustalający, czy badana obserwacja (osobnik) należy do populacji. Sprawdzane jest
/// </summary>
/// <param name="returningSpecimen"></param>
/// <param name="dateValueName"></param>
/// <param name="days"></param>
public class IsInSharedTimeWindowFilter(
    Specimen returningSpecimen,
    string dateValueName,
    int days) : IPopulationFilter
{
    private readonly DateTime returningSpecimenFirstDate = returningSpecimen
        .Observations
        .Select(o => o.GetValue<DateTime>(dateValueName))
        .Min()
        .Date;

    public bool IsAccepted(Specimen specimen)
    {
        var date = specimen
            .Observations
            .Select(o => o.GetValue<DateTime>(dateValueName))
            .Min()
            .Date;

        var daysDifference = Math.Abs((date - returningSpecimenFirstDate).Days);

        return daysDifference <= days;
    }
}