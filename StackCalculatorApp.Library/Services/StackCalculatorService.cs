using StackCalculatorApp.Library.Extensions;

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

    public double EvaluateExpression(string expression)
    {
        stack.Clear();
        operatorStack.Clear();

        string[] tokens = expression.Split(' ');

        foreach (string token in tokens)
        {
            if (double.TryParse(token, out double operand))
            {
                stack.Push(operand);
            }
            else if (token.IsMathOperator())
            {
                if (operatorStack.Count() > 0)
                {
                    if (IsAPriority(token))
                        operatorStack.Push(token);
                    else
                    {
                        while (!IsAPriority(token))
                        {
                            PerformOperation();
                        }

                        operatorStack.Push(token);
                    }
                }
                else
                    operatorStack.Push(token);
            }
            else if (token == "(")
                operatorStack.Push(token);
            else if (token == ")")
            {
                while (operatorStack.Peek() != parentheses[token])
                {
                    PerformOperation();
                }
                operatorStack.Pop();
            }
            else
                throw new ArgumentException("Troubles while TryParsing token: " + token);
        }

        while (operatorStack.Count() > 0)
            PerformOperation();
        return stack.Pop();
    }

    private bool IsAPriority(string currentOp)
    {
        if (currentOp.IsParenthesis() || operatorStack.Count == 0 || operatorStack.Peek() == "(")
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
