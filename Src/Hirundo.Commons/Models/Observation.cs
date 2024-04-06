using Hirundo.Commons.Helpers;

namespace Hirundo.Commons.Models;

/// <summary>
///     Klasa reprezentująca zaobserwowanego osobnika w danym miejscu i czasie.
///     Obserwacja charakteryzuje się zestawem wartości zapisanych w bazie danych.
///     Obserwacja zawsze jest związana z konkretnym osobnikiem. Obserwacja jest
///     związana z zestawem kolumn w bazie danych, które reprezentują wartości kluczowe
///     oraz wartości pomiarowe.
/// </summary>
public class Observation
{
    public string Ring { get; init; } = string.Empty;
    public DateTime Date { get; init; } = DateTime.MinValue;

    private IList<string> _headers = [];
    private readonly IList<object?> _values = [];
    public IList<string> Headers
    {
        get => _headers;
        init => _headers = value.ToList();
    }
    public IList<object?> Values
    {
        get => _values;
        init => _values = value.ToList();
    }
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
    public Observation(IList<string> names, IList<object?> values)
    {
#pragma warning disable IDE0305
        Headers = names.ToList();
        Values = values.ToList();
#pragma warning restore IDE0305
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
        return Values[index];
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
        for (var i = 0; i < Headers.Count; i++)
        {
            if (string.Equals(Headers[i], columnName, StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }

        return -1;
    }



    /// <summary>
    ///     Zwraca listę typów.
    /// </summary>
    /// <returns></returns>
    public Type?[] GetTypes()
    {
        return Values.Select(x => x?.GetType()).ToArray();
    }

    public object?[] SelectValues(string[] columnNames)
    {
        ArgumentNullException.ThrowIfNull(columnNames);

        var result = new object?[columnNames.Length];

        for (var i = 0; i < columnNames.Length; i++)
        {
            result[i] = GetValue(columnNames[i]);
        }

        return result;
    }

    public int?[] SelectIntValues(string[] columnNames)
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
        Headers.Add(columnName);
        Values.Add(columnValue);
    }
}