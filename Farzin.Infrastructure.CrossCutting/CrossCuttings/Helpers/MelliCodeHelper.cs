using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farzin.Infrastructure.CrossCutting.Helpers
{
    public static class MelliCodeHelper
    {
        public static bool IsMelliCodeValid(string melliCode)
        {
            var code = melliCode.Replace("-", "");
            if (code.Length > 10 || code.Length < 8)
                return false;

            var codeArray = code.Reverse().ToArray();
            if (codeArray.Any(a => char.IsDigit(a) == false))
                return false;

            int calc = 0;
            for (int i = 1; i < codeArray.Length; i++)
            {
                calc += (int)char.GetNumericValue(codeArray[i]) * (i + 1);
            }
            calc = calc % 11;

            var controlDigit = (int)char.GetNumericValue(codeArray[0]);
            if (calc < 2)
                return calc == controlDigit;
            else
                return (11 - calc) == controlDigit;
        }
    }
}
