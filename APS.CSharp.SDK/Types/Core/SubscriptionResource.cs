using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.SDK.Types.Core
{

    [Structure]
    public class SubscriptionResource
    {
        /// <summary>
        /// Identifier of subscription resource structure
        /// </summary>
        [Property(Required = true)]
        public string Id { get; set; }

        /// <summary>
        /// Subscription Resource title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// APS resource Id. When a subscription line refers to a particular APS resource - id of the resource.
        /// </summary>
        public string ApsId { get; set; }

        /// <summary>
        /// APS type related to a resource.
        /// </summary>
        public string ApsType { get; set; }

        /// <summary>
        /// If a subscription line refers to some property of an APS resource - the property.
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Usage of a subscription resource. Absence of the property means that usage can’t be calculated.
        /// </summary>
        public int Usage { get; set; }

        /// <summary>
        /// Limit of a subscription resource. Absence of the property means that the resource has no limit (unlimited).
        /// </summary>
        public int Limit { get; set; }
        public bool Autoprovisioning { get; set; }

        /// <summary>
        /// Units to measure limits and usage to resource, such as unit,b,kb,mb,gb and so on
        /// </summary>
        [Property(Required = true, Pattern = "unit|b|kb|mb|gb|.*")]
        public string Unit { get; set; }
    }
}
