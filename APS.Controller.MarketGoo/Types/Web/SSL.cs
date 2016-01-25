using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.Web
{
    [ResourceBase(Id = "http://aps-standard.org/types/web/ssl-certificate/1.0")]
    public class SSL : Core.Resource
    {
        [Property(Required = true)]
        public string Subject { get; set; }

        [Property(Required = true)]
        public string Issuer { get; set; }

        [Property(Required = true)]
        public string Certificate { get; set; }

        [Property(Encrypted = true)]
        public string PrivateKey { get; set; }
    }
}
