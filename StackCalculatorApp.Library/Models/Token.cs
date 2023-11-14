using StackCalculatorApp.Library.Extensions;
namespace StackCalculatorApp.Library.Models;

public class Token
{
    public TokenType Type { get; set; }
    private string _value;
    public string Value 
    {
        get{
            return _value;
        }
        set{
             _value = value;
             DetermineTokenType();
        } 
    }
    public Token(string value = "", TokenType type = TokenType.Invalid)
    {
        if(type == TokenType.Invalid){            
            _value = value;
            DetermineTokenType();
        }else{
            Type = type;
            _value = value;
            
        }
        
    }
    private void DetermineTokenType()
    {
        if (double.TryParse(Value, out _))
        {
            Type = TokenType.Number;
        }

        if (Value.IsMathOperator())
        {
            Type = TokenType.Operator;
        }

        if (Value.IsParenthesis())
        {
            Type = TokenType.Parenthesis;
        }
    }
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Token other = (Token)obj;

        return Value == other.Value && Type == other.Type;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + Type.GetHashCode();
            hash = hash * 23 + Value.GetHashCode();
            return hash;
        }
    }
}