using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.Core
{
    /// <summary>
    /// a type defining a limiting parameter. It is represented by a structure with a single property limit
    /// </summary>
    [Structure]
    public struct Limit
    {
        public int limit { get; set; }
    }
}
