using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SamanageAPI.JsonAttributes;

namespace SamanageAPI.JsonContractResolvers
{
    public sealed class SamanageContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property != null)
            {
                // Check for and handle the JsonNeverSerialize attribute
                var neverSerializeAttribute = property.AttributeProvider.GetAttributes(typeof(JsonNeverSerializeAttribute), true);
                if (neverSerializeAttribute != null && neverSerializeAttribute.Count > 0)
                {
                    property.ShouldSerialize = instance => { return false; };
                    return property;
                }

                // Check for and handle the JsonRequired attribute
                var requiredAttribute = property.AttributeProvider.GetAttributes(typeof(JsonRequiredAttribute), true);
                if (requiredAttribute != null && requiredAttribute.Count > 0)
                {
                    property.ShouldSerialize = instance => { return true; };
                    return property;
                }

                // Catch-all for other properties
                property.ShouldSerialize = CreateDefaultPredicate(property.PropertyName);
            }

            return property;
        }

        /// <summary>
        /// Creates a new Predicate to test if a property has been changed for serialization
        /// </summary>
        /// <param name="propertyName">Property name to test for changes</param>
        /// <returns></returns>
        private static Predicate<object> CreateDefaultPredicate(string propertyName)
        {
            return instance =>
            {
                SamanageObject obj = (SamanageObject)instance;
                return obj.ChangedProperties.Contains(propertyName);
            };
        }
    }
}
