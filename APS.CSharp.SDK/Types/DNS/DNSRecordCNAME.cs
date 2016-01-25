using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.SDK.Types.DNS
{
    /// <summary>
    /// A resource of the “DNSRecordCNAME” type represents the “CNAME” type of RR.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/dns/record/cname/1.0")]
    public class DNSRecordCNAME : DNSRecord
    {

        /// <summary>
        /// The “cname” property must be compliant with http://tools.ietf.org/html/rfc1035 for the RR type.
        /// </summary>
        [Property(Format = "host-name", Required = true)]
        public string Cname { get; set; }
    }
}
