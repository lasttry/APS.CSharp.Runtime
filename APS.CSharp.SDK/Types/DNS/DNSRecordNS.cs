using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.SDK.Types.DNS
{
    /// <summary>
    /// A resource of the “DNSRecordNS” type represents the “NS” type of RR.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/dns/record/ns/1.0")]
    public class DNSRecordNS : DNSRecord
    {
        /// <summary>
        /// The “nsdname” property must be compliant with http://tools.ietf.org/html/rfc1035 for the RR type.
        /// </summary>
        [Property(Format = "host-name", Required = true)]
        public string Nsdname { get; set; }
    }
}
