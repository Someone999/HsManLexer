namespace HsManLexer.Exceptions;

public class LexerException : Exception
{
    public LexerException()
    {
    }

    public LexerException(string? message) : base(message)
    {
    }
}