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
    private string[] _names = null!;
    private object?[] _values = null!;

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
        if (index == -1) return null;
        return _values[index];
    }

    /// <summary>
    ///     Zwraca wartość dla podanej nazwy kolumny, rzutując na odpowiedni typ. Case insensitive.
    /// </summary>
    /// <typeparam name="T">Typ wartości.</typeparam>
    /// <param name="columnName">Nazwa kolumny danych.</param>
    /// <returns></returns>
    public T? GetValue<T>(string columnName)
    {
        var value = GetValue(columnName);
        if (value == null) return default!;
        if (value is T valueOfType) return valueOfType;
        return (T)value;
    }

    public decimal? GetDecimal(string columnName)
    {
        var value = GetValue(columnName);
        if (value == null) return null;
        if (value is decimal decimalValue) return decimalValue;
        return DataTypeHelpers.ConvertValue<decimal>(value);
    }

    public int? GetInt(string columnName)
    {
        var value = GetValue(columnName);
        if (value == null) return null;
        if (value is int intValue) return intValue;
        return DataTypeHelpers.ConvertValue<int>(value);
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

        return -1;
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

    public object?[] GetValues(string[] columnNames)
    {
        ArgumentNullException.ThrowIfNull(columnNames);

        var result = new object?[columnNames.Length];

        for (var i = 0; i < columnNames.Length; i++)
        {
            result[i] = GetValue(columnNames[i]);
        }

        return result;
    }

    public int?[] GetIntValues(string[] columnNames)
    {
        ArgumentNullException.ThrowIfNull(columnNames);

        var result = new int?[columnNames.Length];

        for (var i = 0; i < columnNames.Length; i++)
        {
            result[i] = GetInt(columnNames[i]);
        }

        return result;
    }

    public void AddColumn(string columnName, object? columnValue)
    {
        var newNames = new string[_names.Length + 1];
        var newValues = new object?[_values.Length + 1];
        Array.Copy(_names, newNames, _names.Length);
        Array.Copy(_values, newValues, _values.Length);
        newNames[^1] = columnName;
        newValues[^1] = columnValue;
        _names = newNames;
        _values = newValues;
    }
}