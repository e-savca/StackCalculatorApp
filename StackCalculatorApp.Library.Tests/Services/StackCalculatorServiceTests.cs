using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackCalculatorApp.Library.Models;
using StackCalculatorApp.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackCalculatorApp.Library.Services.Tests;

[TestClass]
public class StackCalculatorServiceTests
{
    #region SetUp
    private StackCalculatorService _stackCalculatorService;

    [TestInitialize]
    public void Setup()
    {
        _stackCalculatorService = new StackCalculatorService();
    }
    #endregion

    #region Scenarios: addition, subtraction, multiplication, division

    [TestMethod]
    public void EvaluateExpression_Addition_ReturnsCorrectResult()
    {
        // Arrange
        List<Token> expression = new List<Token>()
        {
               new Token("2", TokenType.Number),
            new Token("+", TokenType.Operator),
            new Token("-3", TokenType.Number)
        };

        // Act
        double result = _stackCalculatorService.EvaluateExpression(expression);

        // Assert
        Assert.AreEqual(-1, result);
    }

    [TestMethod]
    public void EvaluateExpression_Subtraction_ReturnsCorrectResult()
    {
        // Arrange
        List<Token> expression = new List<Token>()
        {
            new Token("5", TokenType.Number),
            new Token("-", TokenType.Operator),
            new Token("3", TokenType.Number)
        };

        // Act
        double result = _stackCalculatorService.EvaluateExpression(expression);

        // Assert
        Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void EvaluateExpression_Multiplication_ReturnsCorrectResult()
    {
        // Arrange
        List<Token> expression = new List<Token>()
        {
            new Token("4", TokenType.Number),
            new Token("*", TokenType.Operator),
            new Token("2", TokenType.Number)
        };

        // Act
        double result = _stackCalculatorService.EvaluateExpression(expression);

        // Assert
        Assert.AreEqual(8, result);
    }

    [TestMethod]
    public void EvaluateExpression_Division_ReturnsCorrectResult()
    {
        // Arrange
        List<Token> expression = new List<Token>()
        {
            new Token("10", TokenType.Number),
            new Token("/", TokenType.Operator),
            new Token("2", TokenType.Number)
        };

        // Act
        double result = _stackCalculatorService.EvaluateExpression(expression);

        // Assert
        Assert.AreEqual(5, result);
    }
    #endregion

    #region Scenario: calculations with balanced and unbalanced brackets (all brackets are set or some opening and closing brackets are missing)
    [TestMethod]
    public void EvaluateExpression_ShouldReturnCorrectResult_WhenCalculationsWithBalancedBrackets()
    {
        // Arrange
        var expression = new List<Token>
            {
                // "3-(2+-3)(4+2*-3)"
                new Token("3", TokenType.Number),
                new Token("-", TokenType.Operator),
                new Token("(", TokenType.Parenthesis),
                new Token("2", TokenType.Number),
                new Token("+", TokenType.Operator),
                new Token("-3", TokenType.Number),
                new Token(")", TokenType.Parenthesis),
                new Token("*", TokenType.Operator),
                new Token("(", TokenType.Parenthesis),
                new Token("4", TokenType.Number),
                new Token("+", TokenType.Operator),
                new Token("2", TokenType.Number),
                new Token("*", TokenType.Operator),
                new Token("-3", TokenType.Number),
                new Token(")", TokenType.Parenthesis)

            };

        // Act
        var result = _stackCalculatorService.EvaluateExpression(expression);

        // Assert
        Assert.AreEqual(1, result);
    }

    [DataTestMethod]
    [DynamicData(nameof(EvaluateExpression_ShouldThrowArgumentException_WhenCalculationsWithUnbalancedBrackets_Data), DynamicDataSourceType.Method)]
    public void EvaluateExpression_ShouldThrowArgumentException_WhenCalculationsWithUnbalancedBrackets(List<Token> expression)
    {
        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => _stackCalculatorService.EvaluateExpression(expression));
    }
    //Data methods
    public static IEnumerable<object> EvaluateExpression_ShouldThrowArgumentException_WhenCalculationsWithUnbalancedBrackets_Data()
    {
        return new[]
        {
            new object[]
            {
                new List<Token>
            {
                // 3*(-(2+-3)(4+2-3)  -- missing closing bracket
                new Token("3", TokenType.Number),
                new Token("*", TokenType.Operator),
                new Token("(", TokenType.Parenthesis),
                new Token("-", TokenType.Operator),
                new Token("(", TokenType.Parenthesis),
                new Token("2", TokenType.Number),
                new Token("+", TokenType.Operator),
                new Token("-3", TokenType.Number),
                new Token(")", TokenType.Parenthesis),
                new Token("(", TokenType.Parenthesis),
                new Token("4", TokenType.Number),
                new Token("+", TokenType.Operator),
                new Token("2", TokenType.Number),
                new Token("-", TokenType.Operator),
                new Token("3", TokenType.Number),
                new Token(")", TokenType.Parenthesis)
            }
            },
            new object[]
            {
                new List<Token>
            {
                // 3*2+-3*(1+1)*3)(4+2*-3)", -- missing opening bracket
                new Token("3", TokenType.Number),
                new Token("*", TokenType.Operator),
                new Token("2", TokenType.Number),
                new Token("+", TokenType.Operator),
                new Token("-3", TokenType.Number),
                new Token("*", TokenType.Operator),
                new Token("(", TokenType.Parenthesis),
                new Token("1", TokenType.Number),
                new Token("+", TokenType.Operator),
                new Token("1", TokenType.Number),
                new Token(")", TokenType.Parenthesis),
                new Token("*", TokenType.Operator),
                new Token("3", TokenType.Number),
                new Token(")", TokenType.Parenthesis),
                new Token("(", TokenType.Parenthesis),
                new Token("4", TokenType.Number),
                new Token("+", TokenType.Operator),
                new Token("2", TokenType.Number),
                new Token("*", TokenType.Operator),
                new Token("-3", TokenType.Number),
                new Token(")", TokenType.Parenthesis)
            }
            }

        };
    }
    #endregion

    #region  Scenario: Special cases (division by 0, lack of a ++ +- operand);
    [TestMethod]
    public void EvaluateExpression_DivisionByZero_ThrowsDivideByZeroException()
    {
        // Arrange
        var expression = new List<Token>
            {
                new Token("10", TokenType.Number),
                new Token("/", TokenType.Operator),
                new Token("0", TokenType.Number)
            };

        // Act & Assert
        Assert.ThrowsException<DivideByZeroException>(() => _stackCalculatorService.EvaluateExpression(expression));
    }

    [TestMethod]
    public void EvaluateExpression_LackOfOperand_ThrowsArgumentException()
    {
        // Arrange
        var expression = new List<Token>
            {
                new Token("10", TokenType.Number),
                new Token("+", TokenType.Operator),
                new Token("-", TokenType.Operator),
                new Token("2", TokenType.Number),
            };

        // Act & Assert
        Assert.ThrowsException<InvalidOperationException>(() => _stackCalculatorService.EvaluateExpression(expression));
    }

    #endregion

    #region Scenario: division with decimal point result (e.g. 5/2).

    [TestMethod]
    public void EvaluateExpression_DivisionWithDecimalPointResult_ReturnsCorrectResult()
    {
        // Arrange
        List<Token> expression = new List<Token>()
            {
                new Token("1", TokenType.Number),
                new Token("/", TokenType.Operator),
                new Token("7", TokenType.Number)
            };

        // Act
        double result = _stackCalculatorService.EvaluateExpression(expression);

        // Assert
        Assert.AreEqual(0.14, Math.Round(result,2));
    }

    #endregion
}