using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.Mail
{
    /// <summary>
    /// A resource of the “SMTPRelay” type represents an SMTP Relay that accepts and delivers email messages over SMTP. 
    /// The SMTP Relay may require a login and password that must be used for SMTP authorization. 
    /// The SMTP Relay may support secure SMTP (SMTPS).
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/mail/relay/1.0")]
    public class SMTPRelay
    {
        /// <summary>
        /// SMTP endpoints that can be used to send email messages (see declaration of type Socket in Mailbox).
        /// </summary>
        [Property(Required = true)]
        public Socket Smtp { get; set; }

        [Property]
        public Credentials Credentials { get; set; }
    }

    public struct Credentials
    {
        /// <summary>
        /// Login for SMTP authorization.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Password for SMTP authorization.
        /// </summary>
        public string Password { get; set; }
    }
}
