using System.Collections;
using HsManLexer.Exceptions;

namespace HsManLexer.Tokens;

public class TokenCollection(IEnumerable<Token> tokens) : IEnumerable<Token>
{
    private readonly List<Token> _tokens = tokens.ToList();

    public IEnumerator<Token> GetEnumerator() => _tokens.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public TokenStream CreateTokenStream() => new TokenStream(_tokens);
}