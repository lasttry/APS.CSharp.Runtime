using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APS.CSharp.SDK;
using APS.CSharp.SDK.Attributes;
using APS.CSharp.SDK.Types.Core;

namespace APS.Controller.IISManagement
{
    [ResourceBase(ApsVersion = "2.0", Id = "http://ingrammmicro.com/IISManagement/application/1.0", Implements = new string[] { "http://aps-standard.org/types/core/application/1.0" })]
    public class application : Application
    {
        [Property(Title ="Username", Description = "Master username to access all the IIS servers")]
        public string Username { get; set; }
        [Property(Title = "Password", Description = "Master password to access all the IIS servers", Encrypted = false)]
        public string Password { get; set; }

        [Relation]
        public APSLink<server> Servers { get; set; }

    }
}
