using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.Core
{
    /// <summary>
    /// a combination of limit and usage together
    /// </summary>
    [Structure]
    public struct Counter
    {
        public int Usage { get; set; }
        public int Limit { get; set; }
    }
}
