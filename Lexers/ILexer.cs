using HsManLexer.Tokens;

namespace HsManLexer.Lexers;

public interface ILexer
{
    IEnumerable<Token> Tokenize(string text);
}