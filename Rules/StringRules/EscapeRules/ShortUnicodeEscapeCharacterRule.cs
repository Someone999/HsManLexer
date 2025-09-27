namespace HsManLexer.Rules.StringRules.EscapeRules;

public class ShortUnicodeEscapeCharacterRule() : GeneralOneLeadInHexCharacterEscapeRule('u', 4);