using APS.CSharp.SDK.Attributes;
using System.Collections.Generic;
using System.Net.Http;

namespace APS.CSharp.SDK.Types.Core
{
    /// <summary>
    /// This type represents a subscription with hosting/SaaS services.
    /// </summary>
    [ResourceBase(ApsVersion = "2.0", Id = "http://aps-standard.org/types/core/subscription/1.0")]
    public class Subscription
    {
        /// <summary>
        /// Trial flag of the subscription
        /// </summary>
        public bool Trial { get; set; }

        /// <summary>
        /// Disabled flag of the subscription
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Subscription name
        /// </summary>
        [Property(Required = false)]
        public string Name { get; set; }

        /// <summary>
        /// Description of the subscription
        /// </summary>
        [Property(Required = false)]
        public string Description { get; set; }

        public SubscriptionResource SubscriptionResource { get; set; }

        [Operation(Verb = HttpVerbs.GET, Path = "/resources")]
        public List<SubscriptionResource> Resources()
        {
            string result = APSCUtility.APSC.SendRequest(APSCPaths.BuildResourcePath(APSCUtility.APSC.InstanceId, "/resources"), HttpMethod.Get);

            return APSCUtility.APSC.ConvertJson2Object<List<SubscriptionResource>>(result);
        }
    }
}
