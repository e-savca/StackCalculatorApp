using StackCalculatorApp.Library.Extensions;
using StackCalculatorApp.Library.Models;

namespace StackCalculatorApp.Library.Services;

class StackCalculatorService
{
    private Stack<double> stack = new Stack<double>();
    private Stack<string> operatorStack = new Stack<string>();
    private readonly Dictionary<string, int> priorities = new Dictionary<string, int>()
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
        stack.Clear();
        operatorStack.Clear();

        foreach (var token in expression)
        {
            if (token.Type == TokenType.Number)
            {
                stack.Push(Convert.ToDouble(token.Value));
            }
            else if (token.Type == TokenType.Operator)
            {
                if (IsAPriority(token.Value))
                    operatorStack.Push(token.Value);
                else
                {
                    while (!IsAPriority(token.Value))
                    {
                        PerformOperation();
                    }
                    operatorStack.Push(token.Value);
                }
            }
            else if (token.Value == "(")
                operatorStack.Push(token.Value);
            else if (token.Value == ")")
            {
                while (operatorStack.Peek() != parentheses[token.Value])
                {
                    PerformOperation();
                }
                operatorStack.Pop();
            }
            else
                throw new ArgumentException("Troubles while TryParsing token: " + token.Value);
        }

        while (operatorStack.Count() > 0)
            PerformOperation();
        return stack.Pop();
    }

    private bool IsAPriority(string currentOp)
    {
        if (operatorStack.Count == 0 || operatorStack.Peek() == "(")
            return true;

        var prevOperator = priorities[operatorStack.Peek()];
        var currentOperator = priorities[currentOp];
        if (currentOperator > prevOperator)
        {
            return true;
        }
        else
            return false;
    }

    private void PerformOperation()
    {
        double operand2 = stack.Pop();
        if (operand2 == 0)
            throw new DivideByZeroException();
        double operand1 = stack.Pop();

        string operatorSymbol = operatorStack.Pop();
        switch (operatorSymbol)
        {
            case "+":
                stack.Push(operand1 + operand2);
                break;
            case "-":
                stack.Push(operand1 - operand2);
                break;
            case "*":
                stack.Push(operand1 * operand2);
                break;
            case "/":
                stack.Push(operand1 / operand2);
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
}
