namespace Hirundo.Databases;

/// <summary>
///     Typ danych, wymagany do rzutowania. Domyślnie <see cref="DataValueType.Undefined" />.
/// </summary>
public enum DataValueType
{
    /// <summary>
    ///     Niezdefiniowany typ danych. Zwraca wartość o ustawionym typie w bazie danych.
    /// </summary>
    Undefined,

    /// <summary>
    ///     Short integer, reprezentuje liczbę całkowitą z zakresu od -32 768 do 32 767. Odpowiada wartości int16 w C#.
    /// </summary>
    ShortInt,

    /// <summary>
    ///     Long integer, reprezentuje liczbę całkowitą z zakresu od -2 147 483 648 do 2 147 483 647. Odpowiada wartości int32
    ///     w C#.
    /// </summary>
    LongInt,

    /// <summary>
    ///     Łańcuch znaków.
    /// </summary>
    String,

    /// <summary>
    ///     Data i czas.
    /// </summary>
    DateTime,

    /// <summary>
    ///     Liczba zmiennoprzecinkowa.
    /// </summary>
    Decimal
}