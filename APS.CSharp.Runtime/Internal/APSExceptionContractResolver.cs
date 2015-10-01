using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.Runtime.Internal
{
    /// <summary>
    /// Handles the serialization of the APSException to an Operations Automation response
    /// </summary>
    internal class APSExceptionContractResolver : DefaultContractResolver
    {
        private IList<string> _propertiesToSerialize = null;

        public APSExceptionContractResolver()
        {
            _propertiesToSerialize = new List<string>(new string[] {
                "code",
                "message"
            });
        }

        protected override IList<JsonProperty> CreateProperties(Type type, Newtonsoft.Json.MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
            return properties.Where(p => _propertiesToSerialize.Contains(p.PropertyName)).ToList();
        }
    }
}
