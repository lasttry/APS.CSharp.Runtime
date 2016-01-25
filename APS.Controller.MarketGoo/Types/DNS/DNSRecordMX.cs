using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.DNS
{

    /// <summary>
    /// A resource of the “DNSRecordMX” type represents the “MX” type of RR and contains its priority.
    /// To prevent conflicts of DNS records, existence of multiple resources of the “DNSRecordMX” type with the same “source” property and “RRstate” = “active” is prohibited, unless they are all required by a single resource.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/dns/record/mx/1.0")]
    public class DNSRecordMX : DNSRecord
    {
        [Property(Required = true)]
        public int Priority { get; set; }

        /// <summary>
        /// Host of the mail exchange according to http://tools.ietf.org/html/rfc1035 for MX RR type.
        /// </summary>
        [Property(Format = "host-name", Required = true)]
        public string Exchange { get; set; }
    }
}
