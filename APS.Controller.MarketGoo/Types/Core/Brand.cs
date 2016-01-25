using APS.CSharp.SDK.Attributes;
using System.Collections.Generic;

namespace APS.CSharp.Test.Types.Core
{
    [ResourceBase(ApsVersion = "2.0", Id = "http://aps-standard.org/types/core/brand/1.0")]
    public class Brand
    {
        [Property(Required = true)]
        public string Name { get; set; }
        [Property(Required = true)]
        public string Domain { get; set; }
        [Property(Required = true, Format = "uri")]
        public string URI { get; set; }

        [Relation]
        public List<BrandConfiguration> Configurations { get; set; }
    }
}
