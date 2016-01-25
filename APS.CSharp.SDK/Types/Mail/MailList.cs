using APS.CSharp.SDK.Attributes;
using System.Collections.Generic;

namespace APS.CSharp.SDK.Types.Mail
{
    /// <summary>
    /// A resource of the “Mail List” type re presents a mail distribution list. 
    /// It receives email messages via the SMTP protocol and resends all accepted messages to the email addresses being declared in the “Mail Recipient” interface (Mail Recipient) to the list’s members.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/mail/list/1.0")]
    public class MailList : MailRecipient
    {
        /// <summary>
        /// List of the email addresses receiving messages that are sent into this mail distribution list.
        /// </summary>
        [Property(Required = false, Description = "List of email addresses receiving messages which are sent into this mail distribution list.")]
        public List<string> Members { get; set; }
    }
}
