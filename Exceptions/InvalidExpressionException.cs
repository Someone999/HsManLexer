namespace HsManLexer.Exceptions;

public class InvalidExpressionException : Exception
{
    public InvalidExpressionException()
    {
    }

    public InvalidExpressionException(string? message) : base(message)
    {
    }
}