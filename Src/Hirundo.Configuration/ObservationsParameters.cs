using Hirundo.Filters.Observations;

namespace Hirundo.Configuration;

public class ObservationsParameters
{
    public IList<IObservationFilter> Conditions { get; init; } = [];
}