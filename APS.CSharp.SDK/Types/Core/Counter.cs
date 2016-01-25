using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.SDK.Types.Core
{
    /// <summary>
    /// a combination of limit and usage together
    /// </summary>
    [Structure]
    public class Counter
    {
        public int Usage { get; set; }
        public int Limit { get; set; }
    }
}
