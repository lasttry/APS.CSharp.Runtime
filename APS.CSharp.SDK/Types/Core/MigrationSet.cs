using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.SDK.Types.Core
{
    [Structure]
    public class MigrationSet
    {
        [Property(Required = true)]
        public string TargetAccount { get; set; }
    }
}
