using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.Core
{
    [Structure]
    public struct NotificationSource
    {
        [Property(Format = "uri", Description = "APS Type of source resources")]
        public string Type { get; set; }

        [Property(Description ="Resource which is source of event")]
        public string Id { get; set; }
    }
}
