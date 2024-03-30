namespace Hirundo.Databases;

/// <summary>
///     Struktura danych reprezentująca referencję do kolumny w bazie danych. Umożliwia przypisanie nazwy kolumny w bazie
///     danych, nazwy kolumny w danych wynikowych, oraz typu danych. Ustawienie typu danych na
///     <see cref="DataValueType.Undefined" /> powoduje zwrócenie wartości o ustawionym typie w bazie danych. Inne typy
///     danych powodują rzutowanie wartości na typ zdefiniowany w <see cref="DataType" />.
/// </summary>
public class ColumnParameters
{
    /// <summary>
    ///     Konstruktor bezparametrowy.
    /// </summary>
    public ColumnParameters()
    {
    }

    /// <summary>
    ///     Konstruktor przyjmujący nazwę kolumny w bazie danych oraz nazwę kolumny w danych wynikowych.
    /// </summary>
    /// <param name="databaseColumn">Nazwa kolumny w bazie danych.</param>
    /// <param name="valueName">Nazwa kolumny w danych wynikowych.</param>
    /// <param name="dataType">Typ danych, wymagany do rzutowania.</param>
    public ColumnParameters(string databaseColumn, string valueName, DataValueType dataType)
    {
        DatabaseColumn = databaseColumn;
        ValueName = valueName;
        DataType = dataType;
    }

    /// <summary>
    ///     Nazwa kolumny w bazie danych.
    /// </summary>
    public string DatabaseColumn { get; set; } = null!;

    /// <summary>
    ///     Nazwa kolumny w danych wynikowych.
    /// </summary>
    public string ValueName { get; set; } = null!;

    /// <summary>
    ///     Typ danych, wymagany do rzutowania. Domyslnie <see cref="DataValueType.Undefined" />.
    /// </summary>
    public DataValueType DataType { get; set; } = DataValueType.Undefined;
}