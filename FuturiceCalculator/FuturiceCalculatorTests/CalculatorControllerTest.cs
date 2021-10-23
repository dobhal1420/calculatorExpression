using FuturiceCalculator.Controllers;
using FuturiceCalculator.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FuturiceCalculatorTests
{
    public class CalculatorControllerTest
    {
        [Fact]
        public async Task TestWhenGetAllCalculatorControllerNullQueryParameter()
        {
            //Arrange
            var service = new Mock<IExpressionParserService>();
            var logger = new Mock<ILogger<CalculatorController>>();
            
            service.Setup(x => x.EvaluateExpression(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<decimal>());
            var controller = new CalculatorController(logger.Object, service.Object);

            //Act
            var result =  await controller.Get(null).ConfigureAwait(false) as StatusCodeResult;

            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);

        }

        [Fact]
        public async Task TestWhenGetAllCalculatorControllerExpressionQueryParameterAsync()
        {
            //Arrange
            var service = new Mock<IExpressionParserService>();
            var logger = new Mock<ILogger<CalculatorController>>();

            service.Setup(x => x.EvaluateExpression(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<decimal>());
            var controller = new CalculatorController(logger.Object, service.Object);
            string expressionQuery = "MSoyLTMvNCs1KjYtNyo4KzkvMTA=";

            //Act
            var result = await controller.Get(expressionQuery).ConfigureAwait(false) as OkObjectResult;

            service.Verify(x => x.EvaluateExpression(expressionQuery), Times.Once);

        }
    }
}
