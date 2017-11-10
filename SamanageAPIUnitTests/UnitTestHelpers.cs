using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SamanageAPIUnitTests
{
    public static class UnitTestHelpers
    {
        public static DateTime? NullableDateTimeConvert(object value)
        {
            if (value == null)
                return null;

            else if (string.IsNullOrEmpty(value as string))
                return null;

            else
                return Convert.ToDateTime(value as string);
        }

        public static string NullableString(object value)
        {
            if (value == null)
                return null;
            else
                return value.ToString();
        }

        public static Dictionary<string, object> PropertiesToDictionary(object obj)
        {
            return obj.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(obj, null));
        }
    }
}
