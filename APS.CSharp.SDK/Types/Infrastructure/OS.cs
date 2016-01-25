using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.SDK.Types.Infrastructure
{
    [Structure]
    public class OS
    {
        [Property(Description = "Operating system name", Pattern = "Windows|Linux|MacOS|.+")]
        public string Properties { get; set; }

        [Property(Description = "Operation system distribution", Pattern = "redhat|centos|debian|ubuntu|cloudlinux|.+")]
        public string Name { get; set; }

        [Property(Description = "Operation system edition", Pattern = "datacenter|server|desktop|.*")]
        public string Edition { get; set; }

        [Property(Description = "Operation system Version in format <major>.<minor>")]
        public float Version { get; set; }
    }
}
