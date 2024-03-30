namespace Hirundo.Commons.Helpers;

public class HirundoException : Exception
{
    public HirundoException(string message) : base(message)
    {
    }

    public HirundoException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public HirundoException()
    {
    }
}