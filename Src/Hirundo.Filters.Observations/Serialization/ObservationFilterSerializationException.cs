namespace Hirundo.Filters.Observations.Serialization;

public class ObservationFilterSerializationException : Exception
{
    public ObservationFilterSerializationException()
    {
    }

    public ObservationFilterSerializationException(string message) : base(message)
    {
    }

    public ObservationFilterSerializationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}