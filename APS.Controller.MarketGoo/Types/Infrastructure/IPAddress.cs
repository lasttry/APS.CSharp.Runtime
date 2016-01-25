using APS.CSharp.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.Test.Types.Infrastructure
{
    [ResourceBase(Id = "http://aps-standard.org/types/ip-address/1.0")]
    public class IPAddress : Core.Resource
    {
        /// <summary>
        /// IP address.
        /// </summary>
        [Property(Format = "ip-address")]
        public string Address { get; set; }

        /// <summary>
        /// Subnet netmask in CIDR notation, see http://tools.ietf.org/html/rfc4632.
        /// </summary>
        [Property(Required = true)]
        public string Netmask { get; set; }

        /// <summary>
        /// Gateway host.
        /// </summary>
        public string Gateway { get; set; }

        /// <summary>
        /// IP address type. Either IPv4 or IPv6.
        /// </summary>
        [Property(Required = true)]
        public string Type { get; set; }
    }
}
