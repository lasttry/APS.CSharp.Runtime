using APS.CSharp.SDK.Attributes;
using System.Collections.Generic;

namespace APS.CSharp.Test.Types.Core
{
    /// <summary>
    /// The “User” type represents an end user who has access to a hosting control panel. The user resource contains basic information about an end user, such as login and password, and contains his contact details.
    /// </summary>
    [ResourceBase(ApsVersion = "2.0", Id = "http://aps-standard.org/types/core/user/1.0")]
    public class User : Contact
    {
        /// <summary>
        /// Login to access the control panel
        /// </summary>
        [Property(Required = true)]
        public string Login { get; set; }

        /// <summary>
        /// Password to access the control panel
        /// </summary>
        [Property(Encrypted = true)]
        public string Password { get; set; }

        [Relation(Collection = true)]
        public UserService[] Services { get; set; }
    }
}
