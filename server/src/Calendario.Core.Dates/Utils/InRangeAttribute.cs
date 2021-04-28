using System;
using System.Collections.Generic;
using System.Linq;

namespace Calendario.Core.Dates.Utils
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    public class InRangeAttribute : ArgumentValidationAttribute
    {
        private int minInt;
        private int maxInt;
        public InRangeAttribute(int min, int max)
        {
            this.minInt = min;
            this.maxInt = max;
        }

        public override void Validate(object value, string argumentName)
        {
            if (value is int i)
            {    
                if (i < minInt || i > maxInt) throw new ArgumentOutOfRangeException(argumentName);
            }
            else if (value is IEnumerable<int> en)
            {
                if (en.Any(x => x < minInt || x > maxInt)) throw new ArgumentOutOfRangeException(argumentName, "Sequence contains value out of range.");
            }
                    
        }
    }
}