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

            if (char.IsDigit(c))
            {
                sb.Clear();
                while (char.IsDigit(c))
                {
                    sb.Append(c);
                    c = ++i < expression.Length ? expression[i] : '\0';
                }

                tokens.Add(new Token(sb.ToString(), TokenType.Number));
                i--;
            }
            else if (c == '-')
            {
                if (i == 0 || tokens.Last().Type == TokenType.Operator || tokens.Last().Type == TokenType.Parenthesis)
                {
                    sb.Clear();
                    sb.Append(c);
                }
                else
                {
                    tokens.Add(new Token("-", TokenType.Operator));
                }
            }
            else
            {                
                tokens.Add(new Token(c.ToString(), TokenType.Operator));
            }
        }

        return tokens;
    }
}
