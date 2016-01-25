using APS.CSharp.SDK.Attributes;
using System.Collections.Generic;

namespace APS.CSharp.SDK.Types.DNS
{
    [ResourceBase(Id = "http://aps-standard.org/types/dns/zone/1.1")]
    public class HostedDomain : Domain
    {
        /// <summary>
        /// SOA RR for the domain’s DNS zone
        /// Email of the domain administrator from the record should match Admin_user of the domain’s account
        /// </summary>
        public DNSZone Zone { get; set; }

        /// <summary>
        /// (Obsolete) IP addresses of the internal master servers (obsoleted property)
        /// NOTE
        /// The masters property is obsolete and should not be used in new packages.
        /// </summary>
        [Property(Format = "ip-address")]
        public List<string> Masters { get; set; }

        /// <summary>
        /// IP address - of external master DNS server, POA name-servers in slave mode
        /// </summary>
        [Property(Format = "ip-address")]
        public List<string> ExternalMaster { get; set; }

        /// <summary>
        /// Collection of resource records for the domain’s DNS zone
        /// </summary>
        [Relation(Collection = true)]
        public List<DNSRecord> Records { get; set; }

        [Relation]
        public Core.Subscription Hosting { get; set; }

        [Relation]
        public DNSZone ParentDomain { get; set; }

        [Relation(Collection = true)]
        public List<DNSZone> Subdomains { get; set; }

    }
}
