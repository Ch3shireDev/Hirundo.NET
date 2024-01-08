using Hirundo.Filters.Observations;

namespace Hirundo.Configuration;

public class ObservationsParameters
{
    public IObservationFilter[] Conditions { get; set; } = [];
}