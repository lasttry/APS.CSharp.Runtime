using APS.CSharp.SDK.Attributes;
using System.Collections.Generic;

namespace APS.CSharp.SDK.Types.Infrastructure
{
    /// <summary>
    /// Path element defines to which directories the rule is applied. The path is calculated from the root folder
    /// </summary>
    [Structure]
    public class Directory
    {
        [Property(Required = true)]
        public string Path { get; set; }
        public List<Handler> Handlers { get; set; }
    }
}
