using System.Diagnostics.CodeAnalysis;
using System.Text;
using HsManCommonLibrary.Reader;
using HsManLexer.Tokens;

namespace HsManLexer.Rules;

public class IdentifierLexerRule : ILexerRule
{
    private static bool IsAsciiLetterOrUnderScore(char c)
    {
        return c is >= 'a' and <= 'z' or >= 'A' and <= 'Z' or '_';
    }

    private static bool IsDigit(char c)
    {
        return c is >= '0' and <= '9';
    }

    private static bool IsValidIdentifierChar(char c)
    {
        return IsAsciiLetterOrUnderScore(c) || IsDigit(c);
    }
    
    public bool TryParse(SeekableStringReader reader, [NotNullWhen(true)] out Token? token)
    {
        var start = reader.Position;
        StringBuilder builder = new StringBuilder();
        var ch = reader.PeekChar();
        if (!IsAsciiLetterOrUnderScore(ch))
        {
            token = null;
            return false;
        }
        
        builder.Append(reader.ReadChar());
        while (true)
        {
            ch = reader.PeekChar();
            if (!IsValidIdentifierChar(ch))
            {
                break;
            }
            
            builder.Append(ch);
            reader.ReadChar();
        }
        
        if (builder.Length == 0)
        {
            token = null;
            return false;
        }

        token = new Token(start, builder.ToString(), TokenTypes.Identifier);
        return true;
    }
}