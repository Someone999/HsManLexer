using System.Diagnostics.CodeAnalysis;
using HsManCommonLibrary.Reader;

namespace HsManLexer.Rules.StringRules.EscapeRules;

public interface IEscapeCharacterRule
{
    bool TryReadEscape(SeekableStringReader reader, out char escapeCharacter);
}