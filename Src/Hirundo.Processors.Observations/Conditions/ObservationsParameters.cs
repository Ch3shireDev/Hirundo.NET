namespace Hirundo.Processors.Observations.Conditions;

public class ObservationsParameters
{
    public IList<IObservationFilter> Conditions { get; init; } = [];
}