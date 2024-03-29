using Hirundo.Commons;

namespace Hirundo.Databases;

/// <summary>
///     Interfejs dla klas reprezentujących połączenie z bazą danych. Wszystkie klasy implementujące ten interfejs muszą
///     posiadać metodę GetData().
/// </summary>
public interface IDatabase
{
    public IEnumerable<Observation> GetObservations();
}
