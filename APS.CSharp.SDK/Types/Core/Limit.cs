using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.SDK.Types.Core
{
    /// <summary>
    /// a type defining a limiting parameter. It is represented by a structure with a single property limit
    /// </summary>
    [Structure]
    public class Limit
    {
        public int limit { get; set; }
    }
}
