using Hirundo.Commons;

namespace Hirundo.Processors.Specimens;

/// <summary>
///     Procesor osobników pozwala na zgrupowanie obserwacji w osobniki o ustalonym identyfikatorze obrączki.
/// </summary>
public class SpecimensProcessor(SpecimensProcessorParameters parameters)
{
    /// <summary>
    ///     Zwraca osobniki na podstawie listy obserwacji.
    /// </summary>
    /// <param name="observations"></param>
    /// <returns></returns>
    public IEnumerable<Specimen> GetSpecimens(IEnumerable<Observation> observations)
    {
        return observations
                .GroupBy(x => x.GetValue<string>(parameters.SpecimenIdentifier))
                .Where(pair => !string.IsNullOrWhiteSpace(pair.Key))
                .Select(observationGroup => new Specimen(observationGroup.Key, [..observationGroup]))
            ;
    }
}