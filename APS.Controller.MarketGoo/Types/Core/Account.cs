﻿using APS.CSharp.SDK.Attributes;
using System.Collections.Generic;

namespace APS.CSharp.Test.Types.Core
{
    /// <summary>
    /// This type represents an organization with users where some users have administrative permissions. The account resource must refer to administrative users via a weak link.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/core/account/1.0")]
    public class Account
    {
        /// <summary>
        /// Organization name
        /// </summary>
        [Property(Required = true)]
        public string companyName { get; set; }

        /// <summary>
        /// Postal address of the organization
        /// </summary>
        [Property(Required = true)]
        [Structure(Type = "http://aps-standard.org/types/core/contact/1.0#Address")]
        public Address addressPostal { get; set; }

        /// <summary>
        /// Links with users of the organization, including administrators and service users
        /// </summary>
        [Relation(Collection = true)]
        public List<User> users { get; set; }

        [Relation(Required = true)]
        public Brand brand { get; set; }
    }
}
