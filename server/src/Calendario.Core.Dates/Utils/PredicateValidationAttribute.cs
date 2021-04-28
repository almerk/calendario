using System;
using System.Linq;
namespace Calendario.Core.Dates.Utils
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    public class PredicateValidationAttribute : ArgumentValidationAttribute
    {
        public PredicateValidationAttribute(params Predicate<object>[] restrictions)
        {
            this._restrictions = restrictions;
        }
        private Predicate<object>[] _restrictions;

        public override void Validate(object value, string argumentName)
        {
            if (_restrictions.Any(p => !p(value)))
            {
                throw new ArgumentException(argumentName, "Does not satisfy restrictions.");
            }
        }
    }
}