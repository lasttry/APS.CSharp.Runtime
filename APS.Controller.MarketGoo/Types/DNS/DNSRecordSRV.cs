using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.DNS
{
    /// <summary>
    /// A resource of the “DNSRecordSRV” type represents the “SRV” type of RR. Its “RR type” property must be “SRV”. The “source” property must contain a service and protocol according to http://tools.ietf.org/html/rfc2782.
    /// It must be allowed to create a resource which decidedly means absence of a service(according to http://tools.ietf.org/html/rfc2782). In this case the “priority”, “port”, “weight” properties must contain zero and the “target” property must contain ”.”.
    /// To prevent conflicts of DNS records, existence of multiple resources of the “DNSRecordSRV” type with the same “source” property and “RRstate” = “active” is prohibited, unless they are all required by a single resource.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/dns/record/srv/1.0")]
    public class DNSRecordSRV : DNSRecord
    {
        [Property(Required = true)]
        public int Priority { get; set; }

        [Property(Required = true)]
        public int Weight { get; set; }

        [Property(Required = true)]
        public int Port { get; set; }

        [Property(Required = true)]
        public string Target { get; set; }
    }
}
