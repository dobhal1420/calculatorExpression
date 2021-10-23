using FuturiceCalculator.Service;
using FuturiceCalculatorTests.TestDataGenerator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FuturiceCalculatorTests
{
    public class ExpressionParserServiceTest
    {
        [Theory]
        [ClassData(typeof(TestDataGenerationDataSet))]
        public async Task TestExpressionParserService(string input,decimal expectedOutput)
        {
            //Arrange
            var logger = new Mock<ILogger<ExpressionParserService>>();            
            var expressionParserService = new ExpressionParserService(logger.Object);

            var valueBytes = Encoding.UTF8.GetBytes(input);
            var base64 = Convert.ToBase64String(valueBytes);

            //Act
            var result = await expressionParserService.EvaluateExpression(base64).ConfigureAwait(false);

            Assert.Equal(result, expectedOutput);

        }

        [Theory]
        [InlineData("2/0")]
        public void TestNegativeDataIncorrectFormatExpressionParserService(string input)
        {
            //Arrange
            var logger = new Mock<ILogger<ExpressionParserService>>();
            var expressionParserService = new ExpressionParserService(logger.Object);

            var valueBytes = Encoding.UTF8.GetBytes(input);
            var base64 = Convert.ToBase64String(valueBytes);

            //Act
            Assert.ThrowsAsync<FormatException>(() => expressionParserService.EvaluateExpression(base64));

        }

        [Theory]
        [InlineData("a+b")]
        public void TestNegativeDataVariableExpressionParserService(string input)
        {
            //Arrange
            var logger = new Mock<ILogger<ExpressionParserService>>();
            var expressionParserService = new ExpressionParserService(logger.Object);

            var valueBytes = Encoding.UTF8.GetBytes(input);
            var base64 = Convert.ToBase64String(valueBytes);
            
            //Act
            Assert.ThrowsAsync<EvaluateException>(() => expressionParserService.EvaluateExpression(base64));

        }

        [Theory]
        [InlineData("&+$")]
        public void TestNegativeDataSpecialCharacterExpressionParserService(string input)
        {
            //Arrange
            var logger = new Mock<ILogger<ExpressionParserService>>();
            var expressionParserService = new ExpressionParserService(logger.Object);

            var valueBytes = Encoding.UTF8.GetBytes(input);
            var base64 = Convert.ToBase64String(valueBytes);

            //Act
            Assert.ThrowsAsync<SyntaxErrorException>(() => expressionParserService.EvaluateExpression(base64));

        }

        [Theory]
        [InlineData(null)]
        public void TestNullArgumentDataExpressionParserService(string input)
        {
            //Arrange
            var logger = new Mock<ILogger<ExpressionParserService>>();
            var expressionParserService = new ExpressionParserService(logger.Object);

            //Act
            Assert.ThrowsAsync<ArgumentNullException>(() => expressionParserService.EvaluateExpression(input));

        }
    }
}
