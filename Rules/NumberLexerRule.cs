using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using HsManCommonLibrary.Reader;
using HsManLexer.Tokens;

namespace HsManLexer.Rules;

public class NumberLexerRule : ILexerRule
{
    private TokenType GetTokenTypeByNumber(BigInteger num)
    {
        if (num.GetByteCount() <= sizeof(int))
        {
            return num > int.MaxValue ? TokenTypes.UInt32 : TokenTypes.Int32;
        }

        if (num.GetByteCount() == sizeof(long))
        {
            return num > long.MaxValue ? TokenTypes.UInt64 : TokenTypes.Int64;
        }

        throw new InvalidOperationException("Not supported number.");
    }

    private TokenType GetTokenTypeBySuffix(SeekableStringReader reader, BigInteger num)
    {
        var maybeSuffix = reader.PeekString(2);
        int consumeCount = maybeSuffix.Length;
        if (maybeSuffix.Length == 0)
        {
            return GetTokenTypeByNumber(num);
        }

        if (maybeSuffix.Length >= 2 && maybeSuffix.Equals("UL", StringComparison.OrdinalIgnoreCase))
        {
            reader.ConsumeChars(consumeCount);
            return TokenTypes.UInt64;
        }

        consumeCount = 1;
        switch (maybeSuffix[0])
        {
            case 'L':
            case 'l':
                reader.ConsumeChars(consumeCount);
                return TokenTypes.Int64;
            case 'U':
            case 'u':
                reader.ConsumeChars(consumeCount);
                return TokenTypes.UInt32;
            case 'D':
            case 'd':
                reader.ConsumeChars(consumeCount);
                return TokenTypes.Double;
            case 'F':
            case 'f':
                reader.ConsumeChars(consumeCount);
                return TokenTypes.Float;
            default:
                return GetTokenTypeByNumber(num);
        }
    }

    public bool TryParse(SeekableStringReader reader, [NotNullWhen(true)] out Token? token)
    {
        StringBuilder sb = new StringBuilder();
        var ch = reader.PeekChar();
        if (!char.IsDigit(ch))
        {
            token = null;
            return false;
        }

        var startPos = reader.Position;

        sb.Append(reader.ReadChar());
        bool hasDot = false;

        while (true)
        {
            ch = reader.PeekChar();
            if (char.IsDigit(ch))
            {
                sb.Append(ch);
                reader.Read();
                continue;
            }

            if (ch != '.')
            {
                break;
            }

            if (hasDot)
            {
                break;
            }
            
            var next = reader.PeekChar(1);
            if (!char.IsDigit(next))
            {
                break;
            }
            
            sb.Append(ch);
            reader.Read();
        }
        
        BigInteger bi = BigInteger.Parse(sb.ToString());
        token = new Token(startPos, sb.ToString(), GetTokenTypeBySuffix(reader, bi));
        return true;
    }
}