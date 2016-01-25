using APS.CSharp.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.Test.Types.DNS
{
    /// <summary>
    /// The “DNSRecord” type represents the interface for a resource record (RR) of a domain name as defined in http://tools.ietf.org/html/rfc1035, but without destination (RDATA) and type (TYPE). 
    /// A DNS record requires a DNS zone (Hosted Domain) and must have a source and TTL. It must be possible to activate/deactivate an existing DNS record by defining the “RRState” property. 
    /// The RR must be added to/removed from the hosted domain during this operation.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/dns/record/1.0")]
    public class DNSRecord
    {
        /// <summary>
        /// Name of the record. It must be compliant with NAME from http://tools.ietf.org/html/rfc1035.
        /// </summary>
        public string Source { get; set; }
        public int TTL { get; set; }

        /// <summary>
        /// State of a RR.
        /// active: the RR exists in the DNS zone
        /// inactive: the RR does not exist in the DNS zone
        /// any other state
        /// RRs in active and inactive states must be displayed to the user.
        /// </summary>
        [Property(Required = true)]
        public string RRState { get; set; }
    }
}
