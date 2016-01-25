using APS.CSharp.SDK.Attributes;
using System.Collections.Generic;
using System.Net.Http;

namespace APS.CSharp.SDK.Types.Core
{
    /// <summary>
    /// A resource of the “License” type represents an application license key (bits) which is issued by the license authority. The license key must be cancelled when the resource is destroyed. A license may have features which define what application capabilities are enabled by the license key.
    /// A license may require activation data.In this case the resource must not contain a license key if the activation data is not provided yet.
    /// A license may have start and expiration dates. If it has, the license should be renewed (re-activated) before the expiration date.A new license must have the start date not later than the expiration date of the old one.Events of the “aps:linkChanged” type must be generated on (re-)activation.If a license is not renewed before the expiration date, its status must be changed to “inactive” and events of the “aps:linkChanged” type must be generated on the date.
     /// </summary>
    [ResourceBase(ApsVersion = "2.0", Id = "http://aps-standard.org/types/core/license/1.0")]
    public class License
    {
        /// <summary>
        /// Unique identifier of the application license key.
        /// </summary>
        [Property(Required = true)]
        public string Id { get; set; }

        /// <summary>
        /// Status of the license. It can be
        /// active - the “key” field contains key/bits of active license
        /// inactive - the “key” field contains key/bits of inactive license
        /// any other status
        /// </summary>
        [Property(Required = true)]
        public string Status { get; set; }

        /// <summary>
        /// License bits in Base64 encoding.
        /// </summary>
        [Property(Required = true)]
        public string Key { get; set; }

        /// <summary>
        /// License features.
        /// </summary>
        public List<string> Features { get; set; }

        /// <summary>
        /// The date starting from which the existing license key is active.
        /// </summary>
        [Property(Format = "date-time")]
        public string StartDate { get; set; }

        /// <summary>
        /// The date until the existing license key is active.
        /// </summary>
        [Property(Format = "date-time")]
        public string ExpirationDate { get; set; }

        /// <summary>
        /// The method must fill the “key” property with an active license key based on the activation data, if provided.
        /// </summary>
        /// <param name="activationData"></param>
        /// <returns></returns>
        [Operation(Verb = HttpVerbs.POST, Path = "/activationData")]
        [Param(Kind = ParamSource.Body)]
        public string Activate(string activationData)
        {
            string result = APSCUtility.APSC.SendRequest(APSCPaths.BuildResourcePath(APSCUtility.APSC.InstanceId, "/activationData"),
                HttpMethod.Post, activationData, "text/plain");

            return result;
        }

        /// <summary>
        /// The method must update the “key” property with a new license key based on the existing activation data, if any.
        /// </summary>
        /// <returns></returns>
        [Operation(Verb = HttpVerbs.PUT, Path = "/renew")]
        public string Renew()
        {
            string result = APSCUtility.APSC.SendRequest(APSCPaths.BuildResourcePath(APSCUtility.APSC.InstanceId, "/renew"),
                HttpMethod.Put);

            return result;
        }
    }
}
