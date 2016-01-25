using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.Core
{
    [Structure(Description = "Event notification structure")]
    public struct Notification
    {
        [Property(Format = "uri", Required = true, Description = "Type of event (URI)")]
        public string Type { get; set; }

        [Property(Format = "date-time", Description = "Date-time when event happens")]
        public string Time { get; set; }

        [Property(Description = "Serial number of event (incrementing)")]
        public float Serial { get; set; }

        [Property(Description = "Resource originating the event")]
        public NotificationSource Source { get; set; }
    }
}
