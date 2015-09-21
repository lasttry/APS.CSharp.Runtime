using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace APS.CSharp.Runtime
{
    public class Helper
    {
        /// <summary>
        /// Retrieves one Embedded Resource from the current dll.
        /// </summary>
        /// <param name="resourceName">The resource name to retrieve</param>
        /// <returns>The resource in string format.</returns>
        public static string GetEmbeddedResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string result;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new Exception(string.Format("Missing resource '{0}' from assembly.", resourceName));
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }

        public static bool ContainsProperty(dynamic obj, string key)
        {
            return ((IDictionary<string, object>)obj).ContainsKey(key);
        }
    }
}
