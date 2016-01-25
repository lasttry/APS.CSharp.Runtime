using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.SDK.Types.Core
{
    [Structure]
    public class NotificationSource
    {
        [Property(Format = "uri", Description = "APS Type of source resources")]
        public string Type { get; set; }

        [Property(Description ="Resource which is source of event")]
        public string Id { get; set; }
    }
}
