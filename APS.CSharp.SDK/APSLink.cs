using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.SDK
{
    public class APSLink<T>
    {
        public T Value { get; set; }

        public class aps
        {
            public string link { get; set; }
            public string href { get; set; }
        }

    }
}
