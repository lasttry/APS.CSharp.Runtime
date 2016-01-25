using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.SDK.Types.Infrastructure
{

    /// <summary>
    /// The PHP environment is an environment with an installed PHP interpreter, based on the Base Environment. Applications that are compatible with this environment declare an URL Mapping element within their APP-META.xml.
    /// </summary>
    [ResourceBase(ApsVersion = "2.0", Id = "http://aps-standard.org/types/infrastructure/environment/php/1.0")]
    public class PhpEnvironment : Environment
    {
        public Php Php { get; set; }
    }
}
