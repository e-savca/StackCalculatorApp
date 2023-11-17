using StackCalculatorApp.Library.Extensions;
using StackCalculatorApp.Library.Models;

namespace StackCalculatorApp.Library.Services;

public class StackCalculatorService
{
    private Stack<double> _stack = new Stack<double>();
    private Stack<string> _operatorStack = new Stack<string>();
    private readonly Dictionary<string, int> _priorities = new Dictionary<string, int>()
    {
        { "+", 1 },
        { "-", 1 },
        { "*", 2 },
        { "/", 2 }
    };
    private readonly Dictionary<string, string> parentheses = new Dictionary<string, string>()
    {
        { "(", ")" },
        { ")", "(" }
    };

    public double EvaluateExpression(List<Token> expression)
    {
        _stack.Clear();
        _operatorStack.Clear();

        foreach (var token in expression)
        {
            if (token.Type == TokenType.Number)
            {
                _stack.Push(Convert.ToDouble(token.Value));
            }
            else if (token.Type == TokenType.Operator)
            {
                if (IsAPriority(token.Value))
                    _operatorStack.Push(token.Value);
                else
                {
                    while (!IsAPriority(token.Value))
                    {
                        PerformOperation();
                    }
                    _operatorStack.Push(token.Value);
                }
            }
            else if (token.Value == "(")
                _operatorStack.Push(token.Value);
            else if (token.Value == ")")
            {
                while (_operatorStack.Count() > 0 && _operatorStack.Peek() != "(")
                {
                    PerformOperation();
                }
                if (_operatorStack.All(x => x == "(" || x == ")"))
                {
                    throw new ArgumentException("Mismatched parentheses");
                }

                _operatorStack.Pop();
            }
            else
                throw new ArgumentException("Invalid character in expression: " + token.Value);
        }

        while (_operatorStack.Count() > 0)
            PerformOperation();
        if (_stack.Count() == 0)
            throw new ArgumentException("Stack empty");
        return _stack.Pop();
    }

    private bool IsAPriority(string currentOp)
    {
        if (_operatorStack.Count == 0 || _operatorStack.Peek() == "(")
            return true;

        var prevOperator = _priorities[_operatorStack.Peek()];
        var currentOperator = _priorities[currentOp];
        if (currentOperator >= prevOperator)
        {
            return true;
        }
        else
            return false;
    }

    private void PerformOperation()
    {
        try
        {
            double operand2 = _stack.Pop();
            double operand1 = _stack.Pop();

            string operatorSymbol = _operatorStack.Pop();
            switch (operatorSymbol)
            {
                case "+":
                    _stack.Push(operand1 + operand2);
                    break;
                case "-":
                    _stack.Push(operand1 - operand2);
                    break;
                case "*":
                    _stack.Push(operand1 * operand2);
                    break;
                case "/":
                    if (operand2 == 0)
                        throw new DivideByZeroException();
                    _stack.Push(operand1 / operand2);
                    break;
                default:
                    throw new ArgumentException(
                        "Troubles with operatorSymbol: "
                            + operatorSymbol
                            + "\nAnd operands: "
                            + operand1
                            + " & "
                            + operand2
                    );
            }
        }
        catch (DivideByZeroException)
        {
            throw new DivideByZeroException("DivideByZeroException");
        }
        catch(ArgumentException)
        {
            throw new ArgumentException("ArgumentException");
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException("InvalidOperationException");
        }
        catch (FormatException)
        {
            throw new FormatException("FormatException");
        }
        catch (OverflowException)
        {
            throw new OverflowException("OverflowException");
        }
        catch (Exception)
        {
            throw new Exception("Enter a valid expression!");
        }
    }
}
