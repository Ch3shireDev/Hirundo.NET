namespace Hirundo.Databases;

/// <summary>
///     Struktura danych reprezentująca referencję do kolumny w bazie danych. Umożliwia przypisanie nazwy kolumny w bazie
///     danych, nazwy kolumny w danych wynikowych, oraz typu danych. Ustawienie typu danych na
///     <see cref="DataValueType.Undefined" /> powoduje zwrócenie wartości o ustawionym typie w bazie danych. Inne typy
///     danych powodują rzutowanie wartości na typ zdefiniowany w <see cref="DataValueType" />.
/// </summary>
public class DatabaseColumn
{
    /// <summary>
    ///     Konstruktor bezparametrowy.
    /// </summary>
    public DatabaseColumn()
    {
    }

    /// <summary>
    ///     Konstruktor przyjmujący nazwę kolumny w bazie danych oraz nazwę kolumny w danych wynikowych.
    /// </summary>
    /// <param name="sqlColumnName">Nazwa kolumny w bazie danych.</param>
    /// <param name="dataValueName">Nazwa kolumny w danych wynikowych.</param>
    /// <param name="dataValueType">Typ danych, wymagany do rzutowania.</param>
    public DatabaseColumn(string sqlColumnName, string dataValueName, DataValueType dataValueType)
    {
        SqlColumnName = sqlColumnName;
        DataValueName = dataValueName;
        DataValueType = dataValueType;
    }

    /// <summary>
    ///     Nazwa kolumny w bazie danych.
    /// </summary>
    public string SqlColumnName { get; set; } = null!;

    /// <summary>
    ///     Nazwa kolumny w danych wynikowych.
    /// </summary>
    public string DataValueName { get; set; } = null!;

    /// <summary>
    ///     Typ danych, wymagany do rzutowania. Domyslnie <see cref="DataValueType.Undefined" />.
    /// </summary>
    public DataValueType DataValueType { get; set; } = DataValueType.Undefined;
}