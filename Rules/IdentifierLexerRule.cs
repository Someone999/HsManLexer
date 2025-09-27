using System.Diagnostics.CodeAnalysis;
using System.Text;
using HsManCommonLibrary.Reader;
using HsManLexer.Tokens;

namespace HsManLexer.Rules;

public class IdentifierCharacterRules
{
    private readonly List<char> _invalidChars = new();
    private readonly List<Func<char, bool>> _invalidCharFilters = new();

    private readonly List<char> _invalidFirstChars = new();
    private readonly List<Func<char, bool>> _invalidFirstCharFilters = new();

    // 添加方法
    public void AddInvalidChar(char c) => _invalidChars.Add(c);
    public void AddInvalidCharFilter(Func<char, bool> filter) => _invalidCharFilters.Add(filter);
    
    public void AddInvalidFirstChar(char c) => _invalidFirstChars.Add(c);
    public void AddInvalidFirstCharFilter(Func<char, bool> filter) => _invalidFirstCharFilters.Add(filter);

    // 判断是否有效
    public bool IsValid(char c, bool isFirst)
    {
        if (isFirst)
        {
            return !_invalidFirstChars.Contains(c) && !_invalidFirstCharFilters.Any(f => f(c));
        }
        
        return !_invalidChars.Contains(c) && !_invalidCharFilters.Any(f => f(c));
    }

    // 高级方法示例
    public void UseAsciiCharset()
    {
        _invalidCharFilters.Add(c => c > 127);
        _invalidFirstCharFilters.Add(c => c > 127);
    }

    public void UseIgnoreSpace()
    {
        _invalidChars.Add(' ');
        _invalidChars.Add('\t');
    }

    public void UseNoNumberFirstCharacter()
    {
        _invalidFirstCharFilters.Add(char.IsNumber);
    }

    public void UseUnicodeWithoutSurrogate()
    {
        _invalidFirstCharFilters.Add(char.IsSurrogate);
        _invalidCharFilters.Add(char.IsSurrogate);
    }
}

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