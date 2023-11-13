using StackCalculatorApp.Library.Extensions;
using StackCalculatorApp.Library.IServices;
using StackCalculatorApp.Library.Models;

namespace StackCalculatorApp.Library.Services;

class ConsoleHandler : IUserInputHandler
{
    private string Expression { get; set; } = "";

    public string GetExpression()
    {
        Read();
        return Expression;
    }

    private void Read()
    {
        SayHello();

        foreach (var k in Console.ReadLine() ?? "")
        {
            if (k.IsValidCharacter())
            {
                Expression += k.ToString();
            }
        }
    }

    private void SayHello()
    {
        Console.WriteLine(
            "Hello! This is a Stack Calculator.\n"
                + "Enter your expression, using only operators, operands, and round brackets.\n"
                + "Example: \"(11+18)*20-2\".\n"
                + "Press Enter after entering. \n"
            );

        Console.Write($"Your input: ");
    }
}
