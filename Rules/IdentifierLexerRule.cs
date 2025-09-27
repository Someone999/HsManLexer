using System.Diagnostics.CodeAnalysis;
using System.Text;
using HsManCommonLibrary.Reader;
using HsManLexer.Tokens;

namespace HsManLexer.Rules;

public class IdentifierLexerRule(IdentifierCharacterRules characterRules) : ILexerRule
{
    private readonly IdentifierCharacterRules _characterRules = characterRules;
    private bool IsValidFirstIdentifierChar(char c)
    {
        return _characterRules.IsValid(c, true);
    }
    
    private bool IsValidIdentifierChar(char c)
    {
        return _characterRules.IsValid(c, false);
    }
    
    public bool TryParse(SeekableStringReader reader, [NotNullWhen(true)] out Token? token)
    {
        var start = reader.Position;
        StringBuilder builder = new StringBuilder();
        var ch = reader.PeekChar();
        if (!IsValidFirstIdentifierChar(ch))
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