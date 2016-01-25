using APS.CSharp.SDK.Attributes;
using System.Collections.Generic;

namespace APS.CSharp.Test.Types.Mail
{
    /// <summary>
    /// The “MailRecipient” type is the interface of an object that can receive email messages over SMTP protocol to all specified email addresses. It has one primary email address and may contain a list of email aliases. A MailRecepient must receive email to an email address if the address’ domain is bound to the MailRecepient.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/mail/recipient/1.0")]
    public class MailRecipient
    {
        /// <summary>
        /// Primary email address of the mail recipient.
        /// </summary>
        [Property(Format = "email", Required = true, Description = "Primary email address of the mail recipient.")]
        public string PrimaryEmail { get; set; }

        /// <summary>
        /// List of recipient aliases.
        /// </summary>
        [Property(Format = "email", Description = "List of recipient aliases.")]
        public List<string> Aliases { get; set; }

        /// <summary>
        /// The recipient’s display name.
        /// </summary>
        [Property(Required = false, Description = "The recipient's display name.")]
        public string Name { get; set; }

        /// <summary>
        /// Flag indicating whether the mail recipient is invisible to users. The recipient should be treated as visible if this flag is not specified.
        /// </summary>
        [Property(Description = "Flag indicating whether the mail recipient is invisible in the Global Address List of an organization.")]
        public bool HideFromGlobalAddressList { get; set; }

        /// <summary>
        /// List of operations that are not allowed for the recipient.
        /// The following values can be defined:
        /// PULL-ARCHIVING
        /// PUSH-ARCHIVING
        /// any string
        /// </summary>
        [Property(Description = "List of operations which are not allowed for the recipient. Posible values: PULL-ARCHIVING, PUSH-ARCHIVING, any string.")]
        public List<string> Restrictions { get; set; }

        /// <summary>
        /// List of all domains from a customer’s subscription that are used as the domain part in any email address (primary or alias) of the mail recipient. This list may be empty if all recipient’s emails are not based on domains from the customer’s subscription.
        /// </summary>
        [Relation(Collection = true, Required = false)]
        public List<DNS.Domain> Domains { get; set; }
    }
}
