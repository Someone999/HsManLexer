using HsManCommonLibrary.Reader;
using HsManLexer.Exceptions;
using HsManLexer.Rules;
using HsManLexer.Tokens;

namespace HsManLexer.Lexers;

public class RuleBasedLexer : ILexer
{
    public bool IgnoreUnknownCharacters { get; set; } = false;
    public List<ILexerRule> Rules { get; set; } = new List<ILexerRule>();

    public IEnumerable<Token> Tokenize(string text)
    {
        SeekableStringReader reader = new SeekableStringReader(text);
        while (!reader.EndOfString)
        {
            bool parsed = false;
            foreach (var lexerRule in Rules)
            {
                if (!lexerRule.TryParse(reader, out var token))
                {
                    continue;
                }
                

                parsed = true;
                if (token is IgnoredToken)
                {
                    continue;
                }
                
                yield return token;
            }

            if (parsed)
            {
                continue;
            }
            
            if (IgnoreUnknownCharacters)
            {
                reader.Read();
                continue;
            }
            
            throw new LexerException($"Unexpected character \"{reader.PeekChar()}\".");
        }
    }
}