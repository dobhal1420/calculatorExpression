using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuturiceCalculator.Service
{
    public interface IExpressionParserService
    {
        Task<decimal> EvaluateExpression(string expression);
    }
}
