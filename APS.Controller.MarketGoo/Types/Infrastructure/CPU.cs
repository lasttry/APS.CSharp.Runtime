using APS.CSharp.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.Test.Types.Infrastructure
{
    [Structure]
    public struct CPU
    {
        [Property(Description = "Number of CPUcores")]
        public int Number { get; set; }
        [Property(Description = "CPU Power in  MHz")]
        public float Power { get; set; }
    }
}
