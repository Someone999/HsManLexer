namespace HsManLexer.Tokens;

public static class TokenTypes
{
    public static TokenType Byte { get; } = TokenType.Create<byte>();
    public static TokenType SByte { get; } = TokenType.Create<sbyte>();
    public static TokenType Int16 { get; } = TokenType.Create<short>();
    public static TokenType Int32 { get; } = TokenType.Create<int>();
    public static TokenType Int64 { get; } = TokenType.Create<long>();
    public static TokenType UInt16 { get; } = TokenType.Create<ushort>();
    public static TokenType UInt32 { get; } = TokenType.Create<uint>();
    public static TokenType UInt64 { get; } = TokenType.Create<ulong>();
    public static TokenType Float { get; } = TokenType.Create<float>();
    public static TokenType Double { get; } = TokenType.Create<double>();
    public static TokenType String { get; } = TokenType.Create<string>();
    public static TokenType Boolean { get; } = TokenType.Create<bool>();
    public static TokenType Char { get; } = TokenType.Create<char>();
    public static TokenType Null { get; } = new(null, "Null");
    public static TokenType Symbol { get; } = TokenType.Create<string>("Symbol");
    public static TokenType Operator { get; } = TokenType.Create<string>("Operator");
    public static TokenType Identifier { get; } = TokenType.Create<string>("Identifier");
}