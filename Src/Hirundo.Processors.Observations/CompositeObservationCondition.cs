using Hirundo.Commons;

namespace Hirundo.Processors.Observations;

/// <summary>
///     Klasa reprezentująca złożenie wielu warunków na obserwację. Warunki są łączone operatorem logicznym AND,
///     tj. obserwacja jest wybierana, tylko jeśli spełnia wszystkie warunki.
/// </summary>
/// <param name="observations"></param>
public class CompositeObservationCondition : IObservationCondition
{
    public IList<IObservationCondition> Observations { get; }
    public CancellationToken? CancellationToken { get; }

    public CompositeObservationCondition(IObservationCondition[] observations, CancellationToken? token = null)
    {
        ArgumentNullException.ThrowIfNull(observations);
        Observations = observations;
        CancellationToken = token;
    }

    public CompositeObservationCondition(params IObservationCondition[] observations) : this(observations, null) { }

    public bool IsAccepted(Observation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);
        CancellationToken?.ThrowIfCancellationRequested();
        return Observations.All(filter => filter.IsAccepted(observation));
    }
}