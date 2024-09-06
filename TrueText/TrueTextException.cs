namespace TrueText;

public class TrueTextException : Exception
{
    internal TrueTextException(string reason) : base(reason)
    {
    }

    internal TrueTextException(string reason, Exception innerException) : base(reason, innerException)
    {
    }
}