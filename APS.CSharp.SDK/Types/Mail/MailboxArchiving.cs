using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.SDK.Types.Mail
{
    /// <summary>
    /// A resource of the “MailboxArchiving” type represents a regular process called “archiving”.
    /// The process requires a mail recipient (see Mail Recipient) and resends its incoming messages (archives them) to a specified archive target (see Archive Target) which is either a journal address (push archiving) or a special archiving mailbox (pull archiving).
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/mail/archiving/mailbox/1.0")]
    public class MailboxArchiving
    {
        /// <summary>
        /// Enables archiving of messages which are delivered to the mailbox.
        /// </summary>
        [Property(Required = true)]
        public bool InboundArchiving { get; set; }

        /// <summary>
        /// Enables archiving of messages which are sent via mailbox’s MTA.
        /// </summary>
        [Property(Required = true)]
        public bool OutboundArchiving { get; set; }

        /// <summary>
        /// Mail recipient for which incoming and/or outgoing email messages are archived.
        /// </summary>
        [Relation(Required = true)]
        public MailRecipient Source { get; set; }

        /// <summary>
        /// Archive target where incoming and/or outgoing email messages are collected by the mail system for subsequent archiving.
        /// </summary>
        [Relation(Required = true)]
        public ArchiveTarget Target { get; set; }
    }
}
