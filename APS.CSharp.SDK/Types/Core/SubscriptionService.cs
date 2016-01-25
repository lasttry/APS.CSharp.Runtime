using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.SDK.Types.Core
{
    /// <summary>
    /// A resource based on this type is often called ‘context’ or ‘tenant’ service. In a subscription, there is only one resources of this type.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/core/subscription/service/1.0")]
    public class SubscriptionService : Resource
    {
        [Relation(Required = true)]
        public Account Account { get; set; }

        [Relation(Required = true)]
        public Subscription Subscription { get; set; }
    }
}
