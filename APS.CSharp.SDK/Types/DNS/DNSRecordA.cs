using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.SDK.Types.DNS
{
    /// <summary>
    /// A resource of the “DNSRecordA” type represents the “A” type of RR.
    /// To prevent conflicts of DNS records, existence of multiple resources of the “DNSRecordA” type with the same “source” property and “RRstate” = “active” is prohibited, unless they are all required by a single resource.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/dns/record/a/1.0")]
    public class DNSRecordA : DNSRecord
    {
        /// <summary>
        /// The “address” property must be compliant with http://tools.ietf.org/html/rfc1035 for the RR type.
        /// </summary>
        [Property(Format = "ipv4", Required = true)]
        public string Address { get; set; }
    }
}
