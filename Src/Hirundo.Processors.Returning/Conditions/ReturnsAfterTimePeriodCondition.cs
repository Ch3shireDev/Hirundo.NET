using Hirundo.Commons;

namespace Hirundo.Processors.Returning.Conditions;

/// <summary>
///     Filtr zwracający osobniki, które powróciły po określonym czasie, liczonym w dniach. Aby osobnik został
///     uznany za powracający, muszą istnieć co najmniej dwie sąsiednie obserwacje, których różnica
///     dat jest większa lub równa podanej liczbie dni.
/// </summary>
[TypeDescription("ReturnsAfterTimePeriod")]
public class ReturnsAfterTimePeriodCondition : IReturningSpecimenCondition
{
    /// <summary>
    ///     Filtr zwracający osobniki, które powróciły po określonym czasie, liczonym w dniach. Aby osobnik został
    ///     uznany za powracający, muszą istnieć co najmniej dwie sąsiednie obserwacje, których różnica
    ///     dat jest większa lub równa podanej liczbie dni.
    /// </summary>
    /// <param name="dateValueName">Nazwa kolumny danych reprezentującej datę.</param>
    /// <param name="timePeriodInDays">Minimalna liczba dni różnicy pomiędzy obserwacjami.</param>
    public ReturnsAfterTimePeriodCondition(string dateValueName, int timePeriodInDays)
    {
        DateValueName = dateValueName;
        TimePeriodInDays = timePeriodInDays;
    }

    public ReturnsAfterTimePeriodCondition()
    {
    }

    public string DateValueName { get; set; } = null!;
    public int TimePeriodInDays { get; set; } = 300;

    public bool IsReturning(Specimen specimen)
    {
        ArgumentNullException.ThrowIfNull(specimen);

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