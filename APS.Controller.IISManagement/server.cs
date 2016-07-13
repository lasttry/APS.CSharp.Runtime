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
    [ResourceBase(Id = "http://ingrammmicro.com/IISManagement/server/1.0", Implements = new string[] { "http://aps-standard.org/types/core/resource/1.0" })]
    public class server : Resource
    {
        [Property]
        public string Name { get; set; }
        [Property]
        public string IPAddress { get; set; }

        [Relation]
        public application Application { get; set; }

    }
}
