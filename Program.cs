using System.Diagnostics.CodeAnalysis;
using System.Text;
using HsManCommonLibrary.Reader;
using HsManLexer.Lexers;
using HsManLexer.Rules;
using HsManLexer.Rules.StringRules;
using HsManLexer.Rules.StringRules.EscapeRules;
using HsManLexer.Tokens;

namespace HsManLexer;

public class ValueTracker<T> where T: struct
{
    private T _value;

    public ValueTracker(T innerVal)
    {
        _value = innerVal;
    }

    public T Value
    {
        get => _value;
        set => SetValue(value);
    }

    public bool AllowSetValue { get; set; }
    public void SetValue(T value)
    {
        if (!AllowSetValue)
        {
            return;
        }

        _value = value;
    }
    
    public static implicit operator ValueTracker<T>(T val)
    {
        return new ValueTracker<T>(val);
    }
    
    public static implicit operator T(ValueTracker<T> val)
    {
        return val.Value;
    }
}
public class Program
{
    static void Main(string[] args)
    {
        
        
        
        // ListMatchLexerRule symbolRules = new ListMatchLexerRule(TokenTypes.Symbol);
        // symbolRules.AddMatchString("|");
        // symbolRules.AddMatchString("=");
        // symbolRules.AddMatchString(".");
        // RuleBasedLexer lexer = new RuleBasedLexer();
        // lexer.Rules.Add(symbolRules);
        // lexer.Rules.Add(new IdentifierLexerRule());
        // lexer.Rules.Add(new NumberLexerRule());
        //
        // var t = lexer.Tokenize("ax_7562.b.c.d|a=114|b=514");
        // foreach (var token in t)
        // {
        //     Console.WriteLine(token);
        // }
        //
        // Console.WriteLine(""); // Force program to wait the loop to complete
    }
}