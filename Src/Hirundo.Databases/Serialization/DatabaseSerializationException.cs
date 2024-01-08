namespace Hirundo.Databases.Serialization;

public class DatabaseSerializationException : Exception
{
    public DatabaseSerializationException()
    {
    }

    public DatabaseSerializationException(string message) : base(message)
    {
    }

    public DatabaseSerializationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}