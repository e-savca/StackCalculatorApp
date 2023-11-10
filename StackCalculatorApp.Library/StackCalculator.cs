using StackCalculatorApp.Library.Extensions;
using StackCalculatorApp.Library.IServices;
using StackCalculatorApp.Library.Services;

namespace StackCalculatorApp.Library;

public class StackCalculator
{
    private readonly StackCalculator _stackCalculator;
    private readonly IUserInputHandler _userInputHandler;
    public StackCalculator()
    {
        _stackCalculator = new StackCalculator();
        _userInputHandler = new ConsoleHandler();
    }
    public void Start()
    {
        var expression = _userInputHandler.GetExpression().TokenizeExpression();

    }
}
