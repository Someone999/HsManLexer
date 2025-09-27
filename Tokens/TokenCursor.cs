namespace HsManLexer.Tokens;

public class TokenCursor(List<Token> tokens)
{
    private int _pos = -1;

    public Token? Current => _pos >= 0 && _pos < tokens.Count ? tokens[_pos] : null;

    public bool MoveNext()
    {
        if (_pos >= tokens.Count - 1) return false;
        _pos++;
        return true;
    }

    public bool MovePrevious()
    {
        if (_pos <= 0) return false;
        _pos--;
        return true;
    }

    public void Reset() => _pos = -1;
    public void Consume(int count)
    {
        _pos = Math.Min(_pos + count, tokens.Count - 1);
    }

    
    public bool Match(Func<Token?, bool> predicate) => predicate(Current);
    
    public void MatchOrThrow(Func<Token?, bool> predicate, string message)
    {
        if (!predicate(Current))
        {
            throw new TokenMatchFailedException(message);
        }
    }
}