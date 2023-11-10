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

        // Split the expression into individual characters
        char[] chars = expression.ToCharArray();
        
        // Iterate over each character and create a token for it
        for (int i = 0; i < chars.Length; i++)
        {
            char c = chars[i];

            if (char.IsDigit(c))
            {
                // If the character is a digit, parse the number and create a number token
                StringBuilder sb = new StringBuilder();
                sb.Append(c);

                // Keep parsing digits until we reach the end of the number
                while (i + 1 < chars.Length && (char.IsDigit(chars[i + 1]) || chars[i + 1] == '.'))
                {
                    sb.Append(chars[i + 1]);
                    i++;
                }

                tokens.Add(new Token(TokenType.Number, sb.ToString()));
            }
            else if (c == '+')
            {
                tokens.Add(new Token(TokenType.Operator, c.ToString()));
            }
            else if (c == '-')
            {
                // Check if the minus sign is a negative number or a subtraction operator
                if (i + 1 < chars.Length && char.IsDigit(chars[i + 1]))
                {
                    // If the next character is a digit, parse the negative number and create a number token
                    StringBuilder sb = new StringBuilder();
                    sb.Append(c);

                    // Keep parsing digits until we reach the end of the number
                    while (i + 1 < chars.Length && (char.IsDigit(chars[i + 1]) || chars[i + 1] == '.'))
                    {
                        sb.Append(chars[i + 1]);
                        i++;
                    }

                    tokens.Add(new Token(TokenType.Number, sb.ToString()));
                }
                else
                {
                    tokens.Add(new Token(TokenType.Operator, c.ToString()));
                }
            }
            else if (c == '*')
            {
                tokens.Add(new Token(TokenType.Operator, c.ToString()));
            }
            else if (c == '/')
            {
                // Check if the divide sign is before a negative number in parentheses
                bool isNegative = false;
                int j = i + 1;
                while (j < chars.Length && chars[j] == ' ')
                {
                    j++;
                }
                if (j < chars.Length && chars[j] == '-')
                {
                    isNegative = true;
                }

                if (i > 0 && chars[i - 1] == ')' && isNegative)
                {
                    // If the divide sign is before a negative number in parentheses, create a multiplication token instead
                    tokens.Add(new Token(TokenType.Operator, "*"));
                    tokens.Add(new Token(TokenType.Number, "-1"));

                    // Skip the negative sign and whitespace
                    i += 2;
                }
                else
                {
                    tokens.Add(new Token(TokenType.Operator, c.ToString()));
                }
            }
            else if (c == '(')
            {
                tokens.Add(new Token(TokenType.LeftParenthesis, c.ToString()));
            }
            else if (c == ')')
            {
                tokens.Add(new Token(TokenType.RightParenthesis, c.ToString()));
            }
        }

        return tokens;
    }



}


