namespace Hirundo.Commons;

/// <summary>
///     Klasa reprezentująca przedział czasowy, tj. okres od godziny początkowej do godziny końcowej.
///     Dopuszczalne jest podanie godziny końcowej mniejszej niż godzina początkowa, wtedy przedział
///     jest liczony przez północ.
/// </summary>
public class TimeBlock
{
    /// <summary>
    ///     Konstruktor bezparametrowy potrzebny do serializacji.
    /// </summary>
    public TimeBlock()
    {
    }

    /// <summary>
    ///     Konstruktor tworzący przedział czasowy na podstawie godzin.
    /// </summary>
    /// <param name="startHour"></param>
    /// <param name="endHour"></param>
    public TimeBlock(int startHour, int endHour)
    {
        StartHour = startHour;
        EndHour = endHour;
    }

    /// <summary>
    ///     Godzina od której obowiązuje przedział czasowy, liczona od 0 do 23.
    /// </summary>
    public int StartHour { get; set; }

    /// <summary>
    ///     Godzina do której obowiązuje przedział czasowy, liczona od 0 do 23.
    /// </summary>
    public int EndHour { get; set; }
}