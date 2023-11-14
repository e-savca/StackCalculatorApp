using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackCalculatorApp.Library.Extensions;
using StackCalculatorApp.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackCalculatorApp.Library.Extensions.Tests
{
    [TestClass()]
    public class StringExtensionsTests
    {
        [TestMethod()]
        public void IsMathOperatorTest()
        {
            Assert.Fail();
        }
        [TestMethod()]
        public void IsParenthesisTest()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void TokenizeExpression_WhenCalled_ReturnsListOfTokens()
        {
            // Arrange
            string expression = "1+2*3-4/5";
            List<Token> expected = new List<Token>()
            {
                new Token("1", TokenType.Number),
                new Token("+", TokenType.Operator),
                new Token("2", TokenType.Number),
                new Token("*", TokenType.Operator),
                new Token("3", TokenType.Number),
                new Token("-", TokenType.Operator),
                new Token("4", TokenType.Number),
                new Token("/", TokenType.Operator),
                new Token("5", TokenType.Number)
            };

            // Act
            List<Token> actual = expression.TokenizeExpression();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TokenizeExpression_WhenCalledWithNullString()
        {
            // Arrange
            string expression = null;
            // Act
            List<Token> actual = expression!.TokenizeExpression();
        }
        [TestMethod]
        public void TokenizeExpression_WhenCalledWithParenthesis_ReturnsListOfTokens()
        {
            // Arrange
            string expression = "(1+2)*3-4/5";
            List<Token> expected = new List<Token>()
            {
                new Token("(", TokenType.Parenthesis),
                new Token("1", TokenType.Number),
                new Token("+", TokenType.Operator),
                new Token("2", TokenType.Number),
                new Token(")", TokenType.Parenthesis),
                new Token("*", TokenType.Operator),
                new Token("3", TokenType.Number),
                new Token("-", TokenType.Operator),
                new Token("4", TokenType.Number),
                new Token("/", TokenType.Operator),
                new Token("5", TokenType.Number)
            };

            // Act
            List<Token> actual = expression.TokenizeExpression();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TokenizeExpression_WhenCalledWithNegativeNumber_ReturnsListOfTokens()
        {
            // Arrange
            string expression = "1+(-2)*3-4/5";
            List<Token> expected = new List<Token>()
            {
                new Token("1", TokenType.Number),
                new Token("+", TokenType.Operator),
                new Token("-1", TokenType.Number),
                new Token("*", TokenType.Operator),
                new Token("2", TokenType.Number),
                new Token("*", TokenType.Operator),
                new Token("3", TokenType.Number),
                new Token("-", TokenType.Operator),
                new Token("4", TokenType.Number),
                new Token("/", TokenType.Operator),
                new Token("5", TokenType.Number)
            };

            // Act
            List<Token> actual = expression.TokenizeExpression();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TokenizeExpression_WhenCalledWithInvalidCharacter_ThrowsArgumentException()
        {
            // Arrange
            string expression = "1+2*3-4/5^";

            // Act
            List<Token> actual = expression.TokenizeExpression();
        }
    }
}