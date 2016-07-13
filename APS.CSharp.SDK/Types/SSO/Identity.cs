using APS.CSharp.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.SDK.Types.SSO
{
    [ResourceBase(Id = "http://aps-standard.org/types/sso/identity-management/identity/1.0")]
    public class Identity : Core.Resource
    {
        [Property(Description = "a human-friendly name used to refer a user")]
        public string FriendlyName { get; set; }

        [Property(Description = "a unique id that could be used to identify user (even when no identity accounts are presented)")]
        public string UserId { get; set; }

        [Relation(Required = true)]
        public IdentityManagement IdentityManagement { get; set; }

        [Relation(Collection = true)]
        public List<IdentityAccount> IdentityAccounts { get; set; }

        [Operation(Verb = HttpVerbs.DELETE, Path = "/logout")]
        public string Logout()
        {
            string result = APSCUtility.APSC.SendRequest(APSCPaths.BuildResourcePath(APSCUtility.APSC.InstanceId, "/logout"), HttpMethod.Delete);

            return result;
        }
    }
}
