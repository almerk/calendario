using System.Linq;
namespace Calendario.Core.Dates.Utils
{
    public static class AttributeValidationUtils
    {
        public static void ValidateProperties(this object obj)
        {
            var propsWithAttributes = 
                from p in obj.GetType().GetProperties()
                let attrs = p.GetCustomAttributes(typeof(ArgumentValidationAttribute), true)
                where attrs.Length > 0
                select (property:p, attributes: attrs);
            foreach(var pair in propsWithAttributes)
            {
                foreach(ArgumentValidationAttribute attribute in pair.attributes)
                {
                    attribute.Validate(pair.property.GetValue(obj), pair.property.Name);
                }
            }
        }
    }
}
