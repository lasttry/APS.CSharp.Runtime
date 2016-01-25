using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.Core
{
    /// <summary>
    /// a type defining a usage report counter
    /// </summary>
    [Structure]
    public struct Usage
    {
        public int usage { get; set; }
    }
}
