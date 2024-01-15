using Hirundo.Commons;

namespace Hirundo.Processors.Returning.Conditions;

/// <summary>
///     Interfejs dla filtrów powracających osobników. Na podstawie osobnika i jego listy obserwacji określa, czy dany
///     osobnik jest interesującym nas osobnikiem powracającym.
/// </summary>
public interface IReturningSpecimenFilter
{
    /// <summary>
    ///     Sprawdza, czy dany osobnik jest interesującym nas osobnikiem powracającym.
    /// </summary>
    /// <param name="specimen">Osobnik.</param>
    /// <returns>Czy osobnik jest powracający?</returns>
    bool IsReturning(Specimen specimen);
}