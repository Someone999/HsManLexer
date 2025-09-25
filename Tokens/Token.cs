using HsManLexer.Rules;

namespace HsManLexer.Tokens;

public class Token(long position, string text, TokenType tokenType)
{
    public long Position { get; } = position;
    public string Text { get; } = text;
    public TokenType TokenType { get; } = tokenType;

    public override string ToString()
    {
        return $"Token \"{Text}\" ({TokenType.Name}) at {Position}";
    }
}