using HsManLexer.Exceptions;

namespace HsManLexer.Tokens;

public class TokenStream(IEnumerable<Token> tokens)
{
    private readonly Token[] _tokens = tokens.ToArray();
    private int _pos = -1;

    public Token? Current
    {
        get
        {
            if (_pos < 0 || _pos >= _tokens.Length)
            {
                return null;
            }
            
            return _tokens[_pos];
        }
    }

    public bool MoveNext()
    {
        if (_pos >= _tokens.Length - 1)
        {
            return false;
        }

        _pos++;
        return true;
    }
    
    public bool MovePrevious()
    {
        if (_pos <= 0)
        {
            return false;
        }

        _pos--;
        return true;
    }

    public bool Match(Func<Token?, bool> predicate)
    {
        var token = Current;
        return predicate(token);
    }
    

    public void Consume(int count)
    {
        int maxPos = Math.Min(_pos + count, _tokens.Length - 1);
        _pos = maxPos;
    }

    public void MatchOrThrow(Func<Token?, bool> predicate, string message)
    {
        if (predicate(Current))
        {
            return;
        }
        
        throw new TokenMatchFailedException(message);
    }
}