using HsManCommonLibrary.Reader;

namespace HsManLexer.Rules.StringRules.EscapeRules;

public class OctEscapeCharacterRule : IEscapeCharacterRule
{
    private int ParseOctoberSequence(string num)
    {
        if (num.Length != 3)
        {
            throw new ArgumentException("The length of an october escape sequence is always 3", nameof(num));
        }
        
        int sum = 0;
        for (int i = 0; i < 3; i++)
        {
            int digit = num[i] - '0';
            if (digit is < 0 or > 7)
            {
                throw new ArgumentException($"Invalid octal digit at {i} ({digit})");
            }

            sum = sum * 8 + digit;
        }
        
        return sum;
    }
    
    
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
        string strVal = reader.PeekString(3);
        if (strVal.Length < 3)
        {
            return false;
        }

        try
        {
            int octNum = ParseOctoberSequence(strVal);
            escapeCharacter = (char)octNum;
            return true;
        }
        catch (Exception)
        {
            reader.RestorePosition();
            return false;
        }
    }
}