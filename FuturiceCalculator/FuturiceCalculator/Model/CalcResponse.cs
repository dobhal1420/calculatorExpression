using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuturiceCalculator.Model
{
    public class CalcResponse
    {
        public bool Error { get; set; }
        public decimal? Number { get; set; }
    }
}
