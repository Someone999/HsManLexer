namespace HsManLexer.Exceptions;

public class TokenMatchFailedException : Exception
{
    public TokenMatchFailedException()
    {
    }

    public TokenMatchFailedException(string? message) : base(message)
    {
    }

    public TokenMatchFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}