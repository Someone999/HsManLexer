namespace HsManLexer.Tokens;

public class IgnoredToken(long position) : Token(position, string.Empty, TokenType.Create<string>("Ignored"));