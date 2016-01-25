using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.SDK.Types.Mail
{
    [Structure(Type = "http://aps-standard.org/types/mail/mailbox/1.0#Socket")]
    public class Socket
    {
        /// <summary>
        /// Host name or IP address of the corresponding endpoint.
        /// </summary>
        [Property(Format = "host-name", Required = true, Description = "Host name or IP address.")]
        public string Host { get; set; }

        /// <summary>
        /// Port number. If this property is omitted, the protocol’s default value for the specified encryption must be used. For example: 143 for IMAP without encryption, 993 for IMAP with SSL encryption.
        /// </summary>
        [Property(Description = "Port number.If this property is omitted, the protocol's default value for the specified encryption must be used. For example: 143 for IMAP without encryption, 993 for IMAP with SSL encryption.")]
        public int Port { get; set; }

        /// <summary>
        /// Encryption type that is used for data transferring. The possible values: None, SSL, any string. The default value is None.
        /// </summary>
        [Property(Description = "Encryption type which is used for data transferring. Possible values: None, SSL, any string. Defult value is None.")]
        public string Encryption { get; set; }
    }
}
