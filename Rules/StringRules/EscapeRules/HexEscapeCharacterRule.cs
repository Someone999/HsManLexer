namespace HsManLexer.Rules.StringRules.EscapeRules;

public class HexEscapeCharacterRule : GeneralOneLeadInHexCharacterEscapeRule
{
    public HexEscapeCharacterRule() : base('x', 2)
    {
        IgnoreCase = true;
    }
}