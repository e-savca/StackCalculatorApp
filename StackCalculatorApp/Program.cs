using StackCalculatorApp.Library;
using StackCalculatorApp.Library.Extensions;
using StackCalculatorApp.Library.Services;

var calc = new StackCalculator();
calc.Start();

/// testing:

// List<string> expressions = new List<string>()
// {
//     "-3*-5",
//     "3-(2+-3)(4+2*-3)",
//     "-(4+6)",
//     "3*(-(2+-3))(4+2-3)",
//     "3-(2+-3*(1+1)*3)(4+2*-3)",
//     "2*(1+2)*(2*(1+3*2/-(2+2--(1+2))))",
//     "2*(1+2)*(2*(1+3*2/-(2+2--(1+1))))"
// };
// foreach (var expression in expressions)
// {
//     var tokens = expression.TokenizeExpression();
//     var result = new StackCalculatorService().EvaluateExpression(tokens);
//     Console.WriteLine($"Result of {expression}: {result}");
// }
