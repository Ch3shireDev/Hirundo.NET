namespace Hirundo.Commons;

/// <summary>
///     Sezon określa przedział kalendarzowy w roku. Określenie, że jakaś obserwacja
///     zaszła w danym sezonie oznacza, że dla dowolnego roku, została wykonana nie wcześniej
///     niż w dniu StartMonth.StartDay i nie później niż w dniu EndMonth.EndDay.
/// </summary>
public class Season
{
    /// <summary>
    ///     Konstruktor bezparametrowy potrzebny do serializacji.
    /// </summary>
    public Season()
    {
    }

    /// <summary>
    ///     Konstruktor tworzący sezon na podstawie miesięcy i dni.
    /// </summary>
    /// <param name="startMonth">Miesiąc od którego zaczyna się sezon.</param>
    /// <param name="startDay">Dzień od którego zaczyna się sezon</param>
    /// <param name="endMonth">Miesiąc na którym kończy się sezon.</param>
    /// <param name="endDay">Dzień na którym kończy się sezon.</param>
    public Season(int startMonth, int startDay, int endMonth, int endDay)
    {
        StartMonth = startMonth;
        StartDay = startDay;
        EndMonth = endMonth;
        EndDay = endDay;
    }

    /// <summary>
    ///     Miesiąc od którego obowiązuje sezon, liczony od 1 (styczeń) do 12 (grudzień).
    /// </summary>
    public int StartMonth { get; set; }

    /// <summary>
    ///     Dzień miesiąca od którego obowiązuje sezon, liczony od 1 do 31.
    /// </summary>
    public int StartDay { get; set; }

    /// <summary>
    ///     Miesiąc do którego obowiązuje sezon, liczony od 1 (styczeń) do 12 (grudzień).
    /// </summary>
    public int EndMonth { get; set; }

    /// <summary>
    ///     Dzień miesiąca do którego obowiązuje sezon, liczony od 1 do 31.
    /// </summary>
    public int EndDay { get; set; }
}