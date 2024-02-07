namespace Hirundo.Commons;

/// <summary>
///     Klasa reprezentująca zaobserwowanego osobnika w danym miejscu i czasie.
///     Obserwacja charakteryzuje się zestawem wartości zapisanych w bazie danych.
///     Obserwacja zawsze jest związana z konkretnym osobnikiem. Obserwacja jest
///     związana z zestawem kolumn w bazie danych, które reprezentują wartości kluczowe
///     oraz wartości pomiarowe.
/// </summary>
public class Observation
{
    private readonly string[] _names = null!;
    private readonly object?[] _values = null!;

    /// <summary>
    ///     Konstruktor bezparametrowy.
    /// </summary>
    public Observation()
    {
    }

    /// <summary>
    ///     Konstruktor przyjmujący tablicę nazw kolumn oraz tablicę wartości.
    /// </summary>
    /// <param name="names">Nazwy kolumn danych.</param>
    /// <param name="values">Wartości.</param>
    public Observation(string[] names, object?[] values)
    {
        _names = names;
        _values = values;
    }

    /// <summary>
    ///     Zwraca wartość dla podanej nazwy kolumny. Case insensitive.
    /// </summary>
    /// <param name="columnName">Nazwa kolumny danych.</param>
    /// <returns></returns>
    public object? GetValue(string columnName)
    {
        var index = GetIndex(columnName);
        return _values[index];
    }

    /// <summary>
    ///     Zwraca wartość dla podanej nazwy kolumny, rzutując na odpowiedni typ. Case insensitive.
    /// </summary>
    /// <typeparam name="T">Typ wartości.</typeparam>
    /// <param name="columnName">Nazwa kolumny danych.</param>
    /// <returns></returns>
    public T GetValue<T>(string columnName)
    {
        var value = GetValue(columnName);
        if (value == null) return default!;
        return (T)value;
    }

    /// <summary>
    ///     Zwraca indeks dla podanej nazwy kolumny. Case insensitive.
    /// </summary>
    /// <param name="columnName">Nazwa kolumny.</param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    private int GetIndex(string columnName)
    {
        for (var i = 0; i < _names.Length; i++)
        {
            if (string.Equals(_names[i], columnName, StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }

        throw new KeyNotFoundException($"Kolumna '{columnName}' nie występuje w danych.");
    }

    /// <summary>
    ///     Zwraca listę kolumn.
    /// </summary>
    /// <returns></returns>
    public string[] GetHeaders()
    {
        return [.. _names];
    }

    /// <summary>
    ///     Zwraca listę typów.
    /// </summary>
    /// <returns></returns>
    public Type?[] GetTypes()
    {
        return _values.Select(x => x?.GetType()).ToArray();
    }

    public object?[] GetValues()
    {
        return [.. _values];
    }
}