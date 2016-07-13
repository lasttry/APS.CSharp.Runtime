using APS.CSharp.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.SDK.Types.SSO
{
    [ResourceBase(Id = "http://aps-standard.org/types/sso/identity-management/account/1.0")]
    public class IdentityAccount : Core.Resource
    {
        public string Login { get; set; }

        public string Secret { get; set; }

        [Relation(Required = true)]
        public Identity Identity { get; set; }

        [Relation(Required = true)]
        public Provider Provider { get; set; }
    }
}
