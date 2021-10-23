using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuturiceCalculator.Service
{
    public class ExpressionParserService : IExpressionParserService
    {
        private readonly ILogger _logger;

        /// <summary>
        /// <param name="logger">The Logger.</param>
        /// </summary>
        public ExpressionParserService(ILogger<ExpressionParserService> logger)
        {
            _logger = logger;
        }
        public async Task<decimal> EvaluateExpression(string queryExpression)
        {
            try
            {
                byte[] data = Convert.FromBase64String(queryExpression);
                string expression = Encoding.UTF8.GetString(data);

                _logger.Log(LogLevel.Debug, $"Expression : {expression}");
                decimal result = 0;

                await Task.Run(() =>
                {
                    result = decimal.Parse(new DataTable().Compute(expression, null).ToString());
                    _logger.Log(LogLevel.Debug, $"Result : {result}");
                }).ConfigureAwait(false); 

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Evaluate Expression failed", ex);
                throw;
            }


        }
    }
}
