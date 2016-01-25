using APS.CSharp.SDK.Attributes;
using System.Collections.Generic;

namespace APS.CSharp.SDK.Types.Mail
{
    /// <summary>
    /// A resource of the “Mailbox” type represents a user’s personal mailbox which is accessible over IMAP4/POP3 protocols. 
    /// It accepts email messages via the SMTP protocol on email addresses being declared in the “Mail Recipient” interface (Mail Recipient) and delivers them to the user. 
    /// A mailbox must be accessible over the declared protocols (at least IMAP4 or POP3) via the login and password of the user. 
    /// Email delivery over the specified SMTP endpoint must require authorization via the login and password of the user.
    /// </summary>
    /// <remarks>
    /// Mailbox’s user may not have access to the hosting control panel, see User for details.
    /// </remarks>
    [ResourceBase(Id = "http://aps-standard.org/types/mail/mailbox/1.0")]
    public class Mailbox : MailRecipient
    {
        /// <summary>
        /// Protocols that can be used to access the mailbox. Existence of a protocol in the list means its host is defined in the resource properties.
        /// The following values can be defined:
        /// IMAP4
        /// POP3
        /// SMTP
        /// any string
        /// </summary>
        [Property(Required = true, Description = "Protocols which can be used to access a mailbox. Possible values: IMAP4, POP3, SMTP, any string.")]
        public List<string> AccessProtocols { get; set; }

        /// <summary>
        /// List of all POP3 endpoints that provide access to the mailbox.
        /// </summary>
        [Property(Description = "List of all POP3 endpoints which may be used to access the mailbox.")]
        public List<Socket> Pop3 { get; set; }

        /// <summary>
        /// List of all IMAP endpoints that provide access to the mailbox.
        /// </summary>
        [Property(Description = "List of all IMAP endpoints which may be used to access the mailbox.")]
        public List<Socket> Imap4 { get; set; }

        /// <summary>
        /// List of all SMTP endpoints that provide access to the mailbox. At least one SMTP endpoint must be specified.
        /// </summary>
        [Property(Required = true, MinItems = 1, Description = "List of all SMTP endpoints which may be used to access the mailbox. At least one SMTP endpoint must be specified.")]
        public List<Socket> Smtp { get; set; }

        /// <summary>
        /// The user whose login and password are used to access the mailbox via the defined protocols.
        /// </summary>
        [Relation(Required = true)]
        public Core.User User { get; set; }
    }
}
