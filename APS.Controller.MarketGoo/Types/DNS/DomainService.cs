using APS.CSharp.SDK.Attributes;
using System.Collections.Generic;

namespace APS.CSharp.Test.Types.DNS
{
    /// <summary>
    /// Application services than need bindings to domains usually implement this type to get needed relations.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/dns/domain/service/1.0")]
    public class DomainService : Core.Resource
    {
        /// <summary>
        /// required relation with a Domain resource.
        /// </summary>
        [Relation(Required = true)]
        public Domain Domain { get; set; }

        /// <summary>
        /// collection of relations with resources implementing the DNS Record type.
        /// </summary>
        [Relation(Collection = true)]
        public List<DNSRecord> Records { get; set; }

        /// <summary>
        /// an optional relation with the resource implementing the Subscription Service type, often known as management context or tenant resource in the subscription.
        /// </summary>
        [Relation(Required = true)]
        public Core.SubscriptionService Service { get; set; }
    }
}
