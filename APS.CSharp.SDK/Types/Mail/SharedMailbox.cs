using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.SDK.Types.Mail
{
    /// <summary>
    /// A resource of the “Shared Mailbox” type represents a mailbox that is shared between several users. 
    /// It accepts email messages via the SMTP protocol to the email addresses declared in the “Mail Recipient” interface (Mail Recipient).
    /// A shared mailbox must provide access to its messages for different users.
    /// An example of a shared mailbox is a mail-enabled public folder in Exchange.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/mail/shared/1.0")]
    public class SharedMailbox : MailRecipient
    {
    }
}
