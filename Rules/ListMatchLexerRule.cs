using System.Diagnostics.CodeAnalysis;
using System.Text;
using HsManCommonLibrary.Reader;
using HsManLexer.Exceptions;
using HsManLexer.Tokens;

namespace HsManLexer.Rules;

public class ListMatchLexerRule(TokenType tokenType) : ILexerRule
{
    private readonly HashSet<string> _matchList = new();
    private int _maxMatchLength;

    public void AddMatchString(string match)
    {
        _matchList.Add(match);
        _maxMatchLength = Math.Max(_maxMatchLength, match.Length);
    }

    protected virtual bool Match(string text)
    {
        return _matchList.Contains(text);
    }

    public bool TryParse(SeekableStringReader reader, [NotNullWhen(true)] out Token? token)
    {
        var startPos = reader.Position;
        var buffer = new StringBuilder();

        // 尝试读取最多 _maxMatchLength 个字符
        for (int i = 0; i < _maxMatchLength; i++)
        {
            char ch = reader.PeekChar(i); // lookahead
            if (ch == 0)
            {
                break;
            }

            buffer.Append(ch);
        }

        string peeked = buffer.ToString();

        // 从最长到最短尝试匹配
        for (int len = _maxMatchLength; len > 0; len--)
        {
            if (peeked.Length < len)
            {
                continue;
            }

            string candidate = peeked[..len];
            if (!Match(candidate))
            {
                continue;
            }
            
            // 消耗字符
            reader.ConsumeChars(len);
            token = new Token(startPos, candidate, tokenType);
            return true;
        }

        token = null;
        return false;
    }
}