using APS.CSharp.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.SDK.Types.Web
{
    [Structure(Type = "http://aps-standard.org/types/web/website/1.0#User")]
    public class User
    {
        public string Name { get; set; }

        [Property(Encrypted = true)]
        public string Password { get; set; }
    }
}
