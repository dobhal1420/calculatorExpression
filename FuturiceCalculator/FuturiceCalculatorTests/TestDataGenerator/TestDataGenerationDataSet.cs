using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuturiceCalculatorTests.TestDataGenerator
{

    public class TestDataGenerationDataSet : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "1*2-3/4+5*6-7*8+9/10", -23.85M};
            yield return new object[] { "2 * (5 + 5 * 2) / 3 + (6 / 2 + 8)", 21};
            yield return new object[] { "(2+6*3+5-(3*14/7+2)*5)+3", -12 };
            yield return new object[] { "(0-3)/7", -0.42857142857142855M };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
