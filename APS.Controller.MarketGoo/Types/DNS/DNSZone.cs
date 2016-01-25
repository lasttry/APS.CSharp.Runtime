using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.DNS
{
    [Structure]
    public struct DNSZone
    {
        public string AdminEmail { get; set; }

        public int Serial { get; set; }

        public int Refresh { get; set; }

        public int Retry { get; set; }

        public int Expire { get; set; }

        public int Type { get; set; }
    }
}
