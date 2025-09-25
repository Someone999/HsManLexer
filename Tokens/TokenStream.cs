using System.Collections;
using HsManLexer.Exceptions;

namespace HsManLexer.Tokens;

public class TokenStream(IEnumerable<Token> tokens) : IEnumerable<Token?>, IEnumerator<Token?>
{
    private readonly Token[] _tokens = tokens.ToArray();
    private int _pos = -1;

    public void Reset()
    {
        _pos = -1;
    }

    object? IEnumerator.Current => Current;

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

    public IEnumerator<Token?> GetEnumerator()
    {
        return new TokenStream(_tokens);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Dispose()
    {
    }
}