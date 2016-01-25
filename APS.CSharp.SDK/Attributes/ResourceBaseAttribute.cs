using System;
using System.Collections.Generic;

namespace APS.CSharp.SDK.Attributes
{
    public class ResourceBaseAttribute : Attribute
    {

        /// <summary>
        /// The apsVersion element declares the version of the APS runtime the type was written for. This field is mandatory for any type definition.
        /// </summary>
        public string ApsVersion { get; set; }
        /// <summary>
        /// The id element defines a fully-qualified unique Application Type ID in the http://basename/major[.minor] format.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The implements array contains a list of APS Type ID. Each APS Type that can be instantiated by an APS controller must implement at least the http://www.aps-standard.org/core/resource/1.0 type.
        /// </summary>
        public string[] Implements { get; set; }
        public ResourceBaseAttribute()
        {
            ApsVersion = "2.0";
        }
        public ResourceBaseAttribute(string id)
        {
            ApsVersion = "2.0";
            Id = id;
        }
        public string ToHtmlString()
        {
            return string.Format("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp> ApsVersion={0}<br />" +
                "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Id={1}<br/>" +
                "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Implements={2}<br />",
                ApsVersion == null ? "null" : ApsVersion,
                Id == null ? "null" : Id,
                Implements == null ? "null" : String.Join(",", Implements));
        }

    }
}
