using StackCalculatorApp.Library.Extensions;
using StackCalculatorApp.Library.IServices;
using StackCalculatorApp.Library.Services;

namespace StackCalculatorApp.Library;

public class StackCalculator
{
    private readonly StackCalculatorService _stackCalculator;
    private readonly IUserInputHandler _userInputHandler;
    public StackCalculator()
    {
        _stackCalculator = new StackCalculatorService();
        _userInputHandler = new ConsoleHandler();
    }
    public void Start()
    {
        var expression = _userInputHandler.GetExpression().TokenizeExpression();
        var result = _stackCalculator.EvaluateExpression(expression);
        Console.WriteLine($"\nResult: {result}");

    }
}
