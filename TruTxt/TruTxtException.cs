namespace TruTxt;

public class TruTxtException : Exception
{
    internal TruTxtException(string reason) : base(reason)
    {
    }

    internal TruTxtException(string reason, Exception innerException) : base(reason, innerException)
    {
    }
}