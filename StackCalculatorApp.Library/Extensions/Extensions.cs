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
                if (
                    tokens.LastOrDefault()?.Type == TokenType.Number
                    && tokens.LastOrDefault()?.Value.IsMathOperator() == true
                )
                {
                    sb.Append(tokens.Last().Value);
                    tokens.RemoveAt(tokens.Count - 1);
                }
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
                if (
                    (
                        i == 0
                        || (
                            tokens.LastOrDefault()?.Type == TokenType.Operator
                            || tokens.LastOrDefault()?.Type == TokenType.Parenthesis
                        )
                            && expression[i + 1] == '('
                    )
                    || (
                        i > 0
                        && tokens.LastOrDefault()?.Type == TokenType.Operator
                        && expression[i + 1] == '('
                    )
                )
                {
                    tokens.Add(new Token("-1", TokenType.Number));
                    tokens.Add(new Token("*", TokenType.Operator));
                }
                else if (
                    i == 0
                    || tokens.LastOrDefault()?.Type == TokenType.Operator
                    || tokens.LastOrDefault()?.Type == TokenType.Parenthesis
                )
                {
                    sb.Clear();
                    sb.Append(c);
                    tokens.Add(new Token(sb.ToString(), TokenType.Number));
                }
                else
                {
                    tokens.Add(new Token("-", TokenType.Operator));
                }
            }
            else if (c == '(' || c == ')')
            {
                if(tokens.Last().Value == ")" && c == '(')
                {
                    tokens.Add(new Token("*", TokenType.Operator));
                }
                if(tokens.Last().Type == TokenType.Number && c == '(')
                {
                    tokens.Add(new Token("*", TokenType.Operator));
                }
                tokens.Add(new Token(c.ToString(), TokenType.Parenthesis));
            }
            else if (c == '*' || c == '/' || c == '+')
            {
                tokens.Add(new Token(c.ToString(), TokenType.Operator));
            }
            else
            {
                throw new ArgumentException("Invalid character in expression: " + c);
            }
        }

        return tokens;
    }
}
