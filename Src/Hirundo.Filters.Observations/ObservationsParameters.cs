namespace Hirundo.Filters.Observations;

public class ObservationsParameters
{
    public IList<IObservationFilter> Conditions { get; init; } = [];
}