using System;

namespace APS.CSharp.SDK.Attributes
{
    public class ResourceBaseAttribute : Attribute
    {
        public string ApsVersion { get; set; }
        public string Id { get; set; }
        public string Implements { get; set; }
        public ResourceBaseAttribute()
        {
            ApsVersion = "2.0";
        }
        public string ToHtmlString()
        {
            return string.Format("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp> ApsVersion={0}<br />" +
                "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Id={1}<br/>" +
                "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Implements={2}<br />",
                ApsVersion == null ? "null" : ApsVersion,
                Id == null ? "null" : Id,
                Implements == null ? "null" : Implements);
        }

    }
}
