using FuturiceCalculator.Model;
using FuturiceCalculator.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuturiceCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {     

        private readonly ILogger<CalculatorController> _logger;
        private readonly IExpressionParserService _expressionParserService;

        /// <summary>
        /// CalculatorController Constructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="expressionParserService"></param>
        public CalculatorController(ILogger<CalculatorController> logger, IExpressionParserService expressionParserService)
        {
            _logger = logger;
            _expressionParserService = expressionParserService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CalcResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get(string query)
        {
            CalcResponse calcResponse;
            
            try
            {
                query = query ?? throw new ArgumentNullException(nameof(query));

                decimal result = await _expressionParserService.EvaluateExpression(query).ConfigureAwait(false);

                calcResponse = new CalcResponse() { Number = result,Error = false };
                
                return Ok(calcResponse);

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
