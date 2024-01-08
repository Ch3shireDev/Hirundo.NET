namespace Hirundo.Filters.Specimens.Serialization;

public class ReturningSpecimenFilterSerializationException : Exception
{
    public ReturningSpecimenFilterSerializationException()
    {
    }

    public ReturningSpecimenFilterSerializationException(string message) : base(message)
    {
    }

    public ReturningSpecimenFilterSerializationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}