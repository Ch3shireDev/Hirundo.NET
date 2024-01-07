namespace Hirundo.Filters.Observations.Serializers;

public interface IObservationFilterSerializer
{
    public string Serialize(IObservationFilter filter);
    public IObservationFilter Deserialize(string serializedFilter);
}