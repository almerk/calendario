using System;
namespace Calendario.Core.Dates.Utils
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    public class NotNullAttribute : ArgumentValidationAttribute
    {
        public override void Validate(object value, string argumentName)
        {
            if(value==null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}