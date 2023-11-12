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
        Undo();
        bool keepGoing = true;
        while (keepGoing)
        {
            var key = Console.ReadKey();
            switch (key.Key)
            {             
                case ConsoleKey.Enter:
                    Undo();
                    keepGoing = false;
                    break;

                case ConsoleKey.Backspace:                    
                    if (Expression.Length > 0)
                        Expression = Expression.Remove(Expression.Length - 1);
                    Undo();
                    break;
                // case ConsoleKey.: 
                //     if (Expression.Length > 0 && Expression.Last() == '-' && (Expression.Reverse().Skip(1).First().ToString() ?? " ").IsMathOperator())
                //     {
                //         Expression += "1*(";
                //     }
                //     Undo();
                //     break;
                default:
                    if (key.KeyChar.IsValidCharacter())
                    {
                        Expression += key.KeyChar.ToString();
                    }
                    else
                        Undo(true);
                    break;
            }
        }
    }

    private void Undo(bool mistakes = false)
    {
        Console.Clear();
        if (!mistakes)
        {
            Console.WriteLine(
                "Hello! This is a Stack Calculator.\n"
                    + "Enter your expression, using only operators, operands, and round brackets.\n"
                    + "Example: \"(11+18)*20-2\". The correct format of the expression is checked in real time.\n"
                    + "Press Enter after entering. You can also use the backspace to correct the last character.\n"
            );
        }
        else
        {
            Console.WriteLine(
                "Oops! It seems like your expression is not formatted correctly.\n"
                    + "Please make sure to use only operators, operands, and round brackets.\n"
                    + "For example: \"(11+18)*20-2\". Also, remember to press Enter after entering your expression.\n"
                    + "You can use the backspace to correct the last character. Let's try again!\n"
            );
        }
        Console.Write($"Your input: {Expression}");
    }
}