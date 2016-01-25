using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.DNS
{
    /// <summary>
    /// A resource of the “DNSRecordTXT” type represents the “TXT” type of RR.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/dns/record/txt/1.0")]
    public class DNSRecordTXT : DNSRecord
    {

        [Property(Required = true)]
        public string Txt_data { get; set; }
    }
}
