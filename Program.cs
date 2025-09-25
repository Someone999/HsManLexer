using System.Diagnostics.CodeAnalysis;
using System.Text;
using HsManLexer.Lexers;
using HsManLexer.Rules;
using HsManLexer.Tokens;

namespace HsManLexer;


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