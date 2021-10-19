using FuturiceCalculator.Controllers;
using FuturiceCalculator.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace FuturiceCalculatorTests
{
    public class CalculatorControllerTest
    {
        [Fact]
        public void TestWhenGetAllCalculatorControllerNullQueryParameter()
        {
            //Arrange
            var service = new Mock<IExpressionParserService>();
            var logger = new Mock<ILogger<CalculatorController>>();
            
            service.Setup(x => x.EvaluateExpression(It.IsAny<string>()))
                .Returns(It.IsAny<decimal>());
            var controller = new CalculatorController(logger.Object, service.Object);

            //Act
            var result =  controller.Get(null) as StatusCodeResult;

            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);

        }

        [Fact]
        public void TestWhenGetAllCalculatorControllerExpressionQueryParameter()
        {
            //Arrange
            var service = new Mock<IExpressionParserService>();
            var logger = new Mock<ILogger<CalculatorController>>();

            service.Setup(x => x.EvaluateExpression(It.IsAny<string>()))
                .Returns(It.IsAny<decimal>());
            var controller = new CalculatorController(logger.Object, service.Object);
            string expressionQuery = "MSoyLTMvNCs1KjYtNyo4KzkvMTA=";

            //Act
            var result = controller.Get(expressionQuery) as OkObjectResult;

            service.Verify(x => x.EvaluateExpression(expressionQuery), Times.Once);

        }
    }
}
