using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.SDK.Types.Infrastructure
{
    [Structure]
    public class Hardware
    {
        [Property(Description = "Bandwidth in Mbps", Unit = "mbps")]
        public int Bandwidth { get; set; }
        public CPU Cpu { get; set; }
        [Property(Description = "Disk space in Mbytes", Unit = "mb")]
        public int Diskspace { get; set; }

        [Property(Description = "Memory size in Mbytes", Unit = "mb")]
        public int Memory { get; set; }
    }
}
