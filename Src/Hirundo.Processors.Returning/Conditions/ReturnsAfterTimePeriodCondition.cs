using Hirundo.Commons;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Returning.Conditions;

/// <summary>
///     Filtr zwracający osobniki, które powróciły po określonym czasie, liczonym w dniach. Aby osobnik został
///     uznany za powracający, muszą istnieć co najmniej dwie sąsiednie obserwacje, których różnica
///     dat jest większa lub równa podanej liczbie dni.
/// </summary>
[TypeDescription(
    "ReturnsAfterTimePeriod",
    "Czy powrót nastąpił po określonym czasie?",
    "Osobnik wraca nie wcześniej niż po określonej liczbie dni."
    )]
public class ReturnsAfterTimePeriodCondition : IReturningSpecimenCondition
{
    /// <summary>
    ///     Filtr zwracający osobniki, które powróciły po określonym czasie, liczonym w dniach. Aby osobnik został
    ///     uznany za powracający, muszą istnieć co najmniej dwie sąsiednie obserwacje, których różnica
    ///     dat jest większa lub równa podanej liczbie dni.
    /// </summary>
    /// <param name="timePeriodInDays">Minimalna liczba dni różnicy pomiędzy obserwacjami.</param>
    /// 
    public ReturnsAfterTimePeriodCondition(int timePeriodInDays)
    {
        TimePeriodInDays = timePeriodInDays;
    }

    public ReturnsAfterTimePeriodCondition()
    {
    }

    public int TimePeriodInDays { get; set; } = 300;

    public bool IsReturning(Specimen specimen)
    {
        ArgumentNullException.ThrowIfNull(specimen);

        if (specimen.Observations.Count < 2)
        {
            return false;
        }

        var datesAreInOrder = specimen.Observations
            .Select(o => o.Date)
            .OrderBy(d => d)
            .ToArray();

        var datesPairs = datesAreInOrder.Zip(datesAreInOrder.Skip(1));

        var timePeriods = datesPairs.Select(pair => pair.Second - pair.First);

        return timePeriods.Any(period => period.Days >= TimePeriodInDays);
    }
}