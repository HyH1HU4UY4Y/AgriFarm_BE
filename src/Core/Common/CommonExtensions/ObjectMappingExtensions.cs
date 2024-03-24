using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.CommonExtensions
{
    public static class ObjectMappingExtensions
    {
        public static Dictionary<string, object> MapObjectToDictionary<T>(this T obj)
            where T : class
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            // Get all public properties of the object using reflection
            PropertyInfo[] properties = obj.GetType().GetProperties();

            // Iterate through each property and add it to the dictionary
            foreach (PropertyInfo property in properties)
            {
                dict[property.Name] = property.GetValue(obj);
            }

            return dict;
        }
    }
}
