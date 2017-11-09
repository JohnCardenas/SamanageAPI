using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SamanageAPI.Utility
{
    internal static class ExpandoObjectExtensions
    {
        /// <summary>
        /// Deep copies an ExpandoObject though serialization
        /// </summary>
        /// <param name="obj">ExpandoObject to copy</param>
        /// <returns></returns>
        public static ExpandoObject DeepCopy(this ExpandoObject obj)
        {
            string data = JsonConvert.SerializeObject(obj);
            ExpandoObjectConverter converter = new ExpandoObjectConverter();

            return JsonConvert.DeserializeObject<ExpandoObject>(data, converter);
        }

        /// <summary>
        /// Returns true if the specified dynamic has a given property, false otherwise.
        /// </summary>
        /// <param name="expando">ExpandoObject</param>
        /// <param name="key">Property to check for</param>
        /// <returns></returns>
        public static bool HasProperty(this ExpandoObject expando, string key)
        {
            return ((IDictionary<string, object>)expando).ContainsKey(key);
        }

        /// <summary>
        /// Returns a dynamic object property if it is set, otherwise it returns the default value of the type.
        /// </summary>
        /// <typeparam name="T">Type of the property to retrieve</typeparam>
        /// <param name="expando">Dynamic object</param>
        /// <param name="key">Property to retrieve</param>
        /// <returns></returns>
        public static T GetProperty<T>(this ExpandoObject expando, string key)
        {
            if (!HasProperty(expando, key))
                return default(T);

            Type t = typeof(T);
            Type u = Nullable.GetUnderlyingType(t);

            var exp = expando as IDictionary<string, Object>;

            // If we're natively dealing with a Guid, return it
            if ((t == typeof(Guid)) && (exp[key] is Guid))
            {
                return (T)exp[key];
            }
            // If we're dealing with a string but we need a Guid, cast it and return
            else if ((t == typeof(Guid)) && (exp[key] is String))
            {
                Guid g = new Guid((string)exp[key]);
                return (T)Convert.ChangeType(g, t);
            }
            // Special handling for enumerations
            else if (t.IsEnum)
            {
                return (T)Enum.Parse(t, exp[key].ToString());
            }
            // If we have a nullable type we need to handle it differently
            else if (u != null)
            {
                return (exp[key] == null) ? default(T) : (T)Convert.ChangeType(exp[key], u);
            }
            else
            {
                return (T)Convert.ChangeType(exp[key], t);
            }
        }
    }
}
