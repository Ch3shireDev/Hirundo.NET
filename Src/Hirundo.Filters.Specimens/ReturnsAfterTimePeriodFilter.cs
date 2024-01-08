using Hirundo.Commons;

namespace Hirundo.Filters.Specimens;

/// <summary>
///     Filtr zwracający osobniki, które powróciły po określonym czasie, liczonym w dniach. Aby osobnik został
///     uznany za powracający, musi istnieć co najmniej para pomiędzy sąsiednimi obserwacjami, której różnica
///     dat jest większa lub równa podanej liczbie dni.
/// </summary>
/// <param name="dateValueName">Nazwa kolumny danych reprezentującej datę.</param>
/// <param name="timePeriodInDays">Minimalna liczba dni różnicy pomiędzy obserwacjami.</param>
public class ReturnsAfterTimePeriodFilter(string dateValueName, int timePeriodInDays) : IReturningSpecimenFilter
{
    public string DateValueName { get; } = dateValueName;
    public int TimePeriodInDays { get; } = timePeriodInDays;

    public bool IsReturning(Specimen specimen)
    {
        if (specimen.Observations.Count < 2)
        {
            return false;
        }

        var datesAreInOrder = specimen.Observations
            .Select(o => o.GetValue<DateTime>(DateValueName).Date)
            .OrderBy(d => d)
            .ToArray();

        var datesPairs = datesAreInOrder.Zip(datesAreInOrder.Skip(1));

        var timePeriods = datesPairs.Select(pair => pair.Second - pair.First);

        return timePeriods.Any(period => period.Days >= TimePeriodInDays);
    }
}