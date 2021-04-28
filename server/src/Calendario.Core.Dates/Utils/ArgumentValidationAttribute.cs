namespace Calendario.Core.Dates.Utils
{
    public abstract class ArgumentValidationAttribute :System.Attribute
    {
        public abstract void Validate(object value, string argumentName);
    }
}