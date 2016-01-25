using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.Core
{
    [Structure]
    public struct MigrationSet
    {
        [Property(Required = true)]
        public string TargetAccount { get; set; }
    }
}
