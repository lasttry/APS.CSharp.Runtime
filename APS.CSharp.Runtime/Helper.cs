using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;

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

        public static void LogRequest(HttpRequest request, string contents)
        {
            if (!string.IsNullOrEmpty(request.Headers["APS-Controller-URI"]))
            {
                APSRequest(request, contents);
            }

            DataAccess.RequestsAdd(request, contents);
            //string query = string.Format(InsertRequest, DateTime.Now,
            //    request.RawUrl,
            //    request.ContentLength,
            //    request.Headers.ToString(),
            //    request.HttpMethod,
            //    request.Params,
            //    request.ServerVariables,
            //    contents,
            //    request.UserHostName,
            //    request.UserHostAddress);
            //DataAccess.ExecuteNonQuery(query);
        }
        public static void APSRequest(HttpRequest request, string contents)
        {
            DataAccess.APSRequestsAdd(
                DateTime.Now,
                request.Headers["APS-Controller-URI"],
                request.Headers["APS-Identity-ID"],
                request.Headers["APS-Instance-ID"],
                request.Headers["APS-Request-Phase"],
                request.Headers["APS-Transaction-ID"],
                request.HttpMethod,
                request.RawUrl,
                contents
                );

        }

    }
}
