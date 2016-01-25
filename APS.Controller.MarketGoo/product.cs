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
            CSharp.SDK.Types.Core.Resource r = new CSharp.SDK.Types.Core.Resource();
            r.Provision();
            return;

            product p = new product();
            APSC.ProvisionResource(p);

            object j = APSC.GetResources("", "aps/2/resources");
            applications app = APSC.GetResource<applications>("64bc432d-9007-4794-bd7c-c1a466d68ef9");
            APSC.LinkResource("", "name", "");
            APSC.UnlinkResource("", "name", "");
        }

        public override void ProvisionAsync()
        {
            //throw new APSAsync(202, "Accepted", 60);
        }
    }
}
