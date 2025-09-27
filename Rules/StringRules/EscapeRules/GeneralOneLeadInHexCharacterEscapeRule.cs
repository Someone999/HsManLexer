using System.Globalization;
using HsManCommonLibrary.Reader;

namespace HsManLexer.Rules.StringRules.EscapeRules;

public class GeneralOneLeadInHexCharacterEscapeRule(char leadInCharacter, int escapeLength) : IEscapeCharacterRule
{
    public char LeadInCharacter { get;  } = leadInCharacter;
    public int EscapeLength { get; } = escapeLength;
    public bool IgnoreCase { get; set; }

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
        char leadIn = IgnoreCase ? char.ToLower(rawLeadIn) : rawLeadIn;
        char exceptedLeadIn = IgnoreCase ? char.ToLower(LeadInCharacter) : LeadInCharacter;
        if (leadIn != exceptedLeadIn)
        {
            reader.RestorePosition();
            return false;
        }

        reader.Read();
        var strVal = reader.PeekString(EscapeLength);
        if (strVal.Length < EscapeLength || 
            !int.TryParse(strVal, NumberStyles.HexNumber, null, out var realVal))
        {
            reader.RestorePosition();
            return false;
        }
        
        escapeCharacter = (char)realVal;
        reader.ConsumeChars(EscapeLength);
        return true;
    }
}