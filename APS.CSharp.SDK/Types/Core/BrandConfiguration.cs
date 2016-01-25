using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.SDK.Types.Core
{
    public class BrandConfiguration : Resource
    {
        [Relation(Required = true)]
        public Brand Brand { get; set; }
    }
}
