using HsManCommonLibrary.Reader;

namespace HsManLexer.Rules.StringRules.EscapeRules;

public class CommonHexCharacterEscapeRule : IEscapeCharacterRule
{
    public bool TryReadEscape(SeekableStringReader reader, out char escapeCharacter)
    {
        escapeCharacter = '\0';
        char p = reader.PeekChar();
        if (p != '\\')
        {
            return false;
        }

        reader.SavePosition();
        reader.Read();
        char rawLeadIn = reader.PeekChar();

        switch (rawLeadIn)
        {
            case 'a': 
                escapeCharacter = '\a';
                reader.Read();
                return true;
            case 'b':
                escapeCharacter = '\b';
                reader.Read();
                return true;
            case 'f':
                escapeCharacter = '\f';
                reader.Read();
                return true;
            case 'n': 
                escapeCharacter = '\n';
                reader.Read();
                return true;
            case 't':
                escapeCharacter = '\t';
                reader.Read();
                return true;
            case 'r':
                escapeCharacter = '\r';
                reader.Read();
                return true;
            case 'v':
                escapeCharacter = '\v';
                reader.Read();
                return true;
            case '\'':
                escapeCharacter = '\'';
                reader.Read();
                return true;
            case '"':
                escapeCharacter = '"';
                reader.Read();
                return true;
            case '?':
                escapeCharacter = '?';
                reader.Read();
                return true;
            case '\\':
                escapeCharacter = '\\';
                reader.Read();
                return true;
        }

        reader.RestorePosition();
        return false;
    }
}