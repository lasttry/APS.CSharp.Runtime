using APS.CSharp.SDK;
using APS.CSharp.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.Controller.MarketGoo
{
    [ResourceBase(Id = "http://odin.com/MarketGoo/product/1.0")]
    public class product : ResourceBase
    {
        public override void Provision()
        {
            APSC.GetResources("", "aps/2/resources");
        }
    }
}
