using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using StackCalculatorApp.Library.Models;
namespace StackCalculatorApp.Library.Extensions;

public static class Extensions
{
    public static bool IsMathOperator(this string tokenValue)
    {
        return tokenValue == "+" || tokenValue == "-" || tokenValue == "*" || tokenValue == "/";  
    }
    public static bool IsParenthesis(this string tokenValue)
    {
        return tokenValue == "(" || tokenValue == ")";
    }
    public static bool IsValidCharacter(this char keyChar) => "0123456789/*-+()".Contains(keyChar);

    public static List<Token> TokenizeExpression(this string expression)
{
    List<Token> tokens = new List<Token>();
    StringBuilder sb = new StringBuilder();

    for (int i = 0; i < expression.Length; i++)
    {
        char c = expression[i];

        if (char.IsWhiteSpace(c))
        {
            continue;
        }

        if (char.IsDigit(c) || (c == '.' && i + 1 < expression.Length && char.IsDigit(expression[i + 1])))
        {
            sb.Clear();
            do
            {
                sb.Append(c);
                c = ++i < expression.Length ? expression[i] : '\0';
            }
            while (char.IsDigit(c) || c == '.');

            tokens.Add(new Token(sb.ToString(), TokenType.Number));
            i--;
        }
        else if (c == '-' && i + 1 < expression.Length && expression[i + 1] == '(')
        {
            tokens.Add(new Token("1", TokenType.Number));
            tokens.Add(new Token("*", TokenType.Operator));
            tokens.Add(new Token("-1", TokenType.Number));
            i++;
        }
        else
        {
            TokenType type;
            switch (c)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                    type = TokenType.Operator;
                    break;
                case '(':
                case ')':
                    type = TokenType.Parenthesis;
                    break;
                default:
                    throw new ArgumentException($"Invalid character in expression: {c}");
            }
            tokens.Add(new Token(c.ToString(), type));
        }
    }

    return tokens;
}

    


}


