using APS.CSharp.SDK;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.Runtime.Internal
{
    internal class Serialization
    {

        /// <summary>
        /// Serializes the APSException so the APCS can understand what we are sending back
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        internal static string SerializeAPSException(APSException ex)
        {
            var acceptedException = new Dictionary<string, object>();
            acceptedException.Add("code", ex.Code);
            if (!string.IsNullOrEmpty(ex.Message))
                acceptedException.Add("message", ex.Message);
            if (!string.IsNullOrEmpty(ex.Error))
                acceptedException.Add("error", ex.Error);
            if (!string.IsNullOrEmpty(ex.Details))
                acceptedException.Add("details", ex.Details);
            if (!string.IsNullOrEmpty(ex.AdditionalInfo))
                acceptedException.Add("additionalInfo", ex.AdditionalInfo);

            return JsonConvert.SerializeObject(acceptedException, Formatting.None, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
            });
        }
    }
}
