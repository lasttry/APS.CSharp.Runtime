using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.DNS
{
    /// <summary>
    /// A resource of the “Domain” type represents a domain name defined in http://tools.ietf.org/html/rfc1035.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/dns/domain/1.0")]
    public class Domain
    {
        [Property(Required = true)]
        public string Name { get; set; }
    }
}
