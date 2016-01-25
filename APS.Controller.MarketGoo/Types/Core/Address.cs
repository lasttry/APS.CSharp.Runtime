using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.Core
{
    [Structure]
    public struct Address
    {
        public string PostOfficeBox { get; set; }
        public string ExtendedAddress { get; set; }
        public string StreetAddress { get; set; }
        public string Locality { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string CountryName { get; set; }
    }
}
