using Hirundo.Commons;

namespace Hirundo.Filters.Specimens;

/// <summary>
///     Filtr zwracający osobniki, które powróciły po określonym czasie, liczonym w dniach. Aby osobnik został
///     uznany za powracający, musi istnieć co najmniej para pomiędzy sąsiednimi obserwacjami, której różnica
///     dat jest większa lub równa podanej liczbie dni.
/// </summary>
/// <param name="dateValueName">Nazwa kolumny danych reprezentującej datę.</param>
/// <param name="days">Minimalna liczba dni różnicy pomiędzy obserwacjami.</param>
public class SpecimenReturnsAfterTimePeriodFilter(string dateValueName, int days) : IReturningSpecimenFilter
{
    public bool IsReturning(Specimen specimen)
    {
        if (specimen.Observations.Count < 2)
        {
            return false;
        }

        var datesAreInOrder = specimen.Observations
            .Select(o => o.GetValue<DateTime>(dateValueName).Date)
            .OrderBy(d => d)
            .ToArray();

        var datesPairs = datesAreInOrder.Zip(datesAreInOrder.Skip(1));

        var timePeriods = datesPairs.Select(pair => pair.Second - pair.First);

        return timePeriods.Any(period => period.Days >= days);
    }
}