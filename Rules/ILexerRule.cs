using System.Diagnostics.CodeAnalysis;
using HsManCommonLibrary.Reader;
using HsManLexer.Tokens;

namespace HsManLexer.Rules;

public interface ILexerRule
{
    bool TryParse(SeekableStringReader reader, [NotNullWhen(true)] out Token? token);
}