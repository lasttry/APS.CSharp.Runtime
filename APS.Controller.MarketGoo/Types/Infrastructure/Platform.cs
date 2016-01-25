using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.Infrastructure
{
    [Structure]
    public struct Platform
    {
        [Property(Description = "System architecture", Pattern = "x86|x86_64|ia64|arm|.+")]
        public string Arch { get; set; }

        [Property(Description = "System operating system")]
        public OS Os { get; set; }
    }
}
