using System.Diagnostics.CodeAnalysis;
using System.Text;
using HsManCommonLibrary.Reader;
using HsManLexer.Rules.StringRules.EscapeRules;
using HsManLexer.Tokens;

namespace HsManLexer.Rules.StringRules;

public class StringLexerRule : ILexerRule
{
    private List<IEscapeCharacterRule> _escapeCharacterRules = new List<IEscapeCharacterRule>()
    {
        new CommonHexCharacterEscapeRule(),
        new HexEscapeCharacterRule(),
        new ShortUnicodeEscapeCharacterRule(),
        new OctEscapeCharacterRule(),
        new LongUnicodeEscapeCharacterRule()
    };
    public bool TryParse(SeekableStringReader reader, [NotNullWhen(true)] out Token? token)
    {
        StringBuilder builder = new StringBuilder();
        char p = reader.PeekChar();
        if (p != '"')
        {
            token = null;
            return false;
        }

        long pos = reader.Position;
        reader.Read();
        while (true)
        {
            p = reader.PeekChar();
            switch (p)
            {
                case '"':
                    reader.Read();
                    token = new Token(pos, builder.ToString(), TokenTypes.String);
                    return true;
                case '\\':
                    bool suc = false;
                    char character = '\0';
                    foreach (var escapeCharacterRule in _escapeCharacterRules)
                    {
                        if (!escapeCharacterRule.TryReadEscape(reader, out character))
                        {
                            continue;
                        }
                        
                        suc = true;
                        break;
                    }

                    if (!suc)
                    {
                        throw new InvalidOperationException();
                    }

                    builder.Append(character);
                    break;
                default:
                    builder.Append(p);
                    reader.Read();
                    break;
                
            }
        }
    }
}