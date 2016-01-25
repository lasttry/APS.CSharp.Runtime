using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.Core
{
    /// <summary>
    /// The contact resource is based on a vCard contact and contains a subset of its properties. The “Address” structure represents an address of organization or individual.
    /// </summary>
    [ResourceBase(ApsVersion = "2.0", Id = "http://aps-standard.org/types/core/contact/1.0")]
    public class Contact
    {
        [Property(Format = "email")]
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string AdditionalName { get; set; }
        public string FamilyName { get; set; }
        /// <summary>
        /// The full name of a contact as defined by the “fn” class in [HCARD-PROFILE]. If any of the “givenName”, “familyName” or “additionalName” properties is defined, the “fullName” property must be defined as well.
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Any descriptive name of the contact, for example, the full name or nick name.
        /// </summary>
        public string DisplayName { get; set; }
        public string OrganizationName { get; set; }
        public string[] OrganizationUnit { get; set; }
        public string Title { get; set; }
        public string AddressPostal { get; set; }
        public string TelVoice { get; set; }
        public string TelHome { get; set; }
        public string TelWork { get; set; }
        public string TelCell { get; set; }
        public string TelFax { get; set; }
        public string TelPager { get; set; }
        public string Tz { get; set; }
        public string Note { get; set; }
        public string Url { get; set; }
        public Address Addres { get; set; }
    }
}
