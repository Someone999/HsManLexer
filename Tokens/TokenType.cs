namespace HsManLexer.Tokens;

public class TokenType(Type? t, string? typeName = null)
{
    public Type? ClrType { get; } = t;
    public string? Name { get; } = typeName ?? t?.Name;
    public static TokenType Create<T>(string? name = null) => new TokenType(typeof(T), name);

    public bool Is<T>()
    {
        return ClrType == typeof(T);
    }
    
    public bool IsNull() => ClrType is null && Name is null;
}