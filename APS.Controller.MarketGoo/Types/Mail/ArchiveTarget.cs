using APS.CSharp.SDK.Attributes;
using System.Collections.Generic;

namespace APS.CSharp.Test.Types.Mail
{

    /// <summary>
    /// A resource of the “ArchiveTarget” type represents storage where a mail system collects email messages for subsequent archiving. 
    /// An archiving application should implement this type in such a way that a service provider might create its different instances to give different configurations of archiving for subscribers.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/mail/archiving/target/1.0")]
    public class ArchiveTarget
    {
        /// <summary>
        /// Archive target title.
        /// </summary>
        [Property(Required = false)]
        [Access(Owner = true, Referrer = true)]
        public string Title { get; set; }

        /// <summary>
        /// Flag that being set to “true” indicates that the archive target supports “push” archiving.
        /// </summary>
        [Property(Required = true, Default = false)]
        [Access(Owner = true, Referrer = true)]
        public bool Push { get; set; }

        /// <summary>
        /// Flag that being set to “true” indicates that the archive target supports “pull” archiving.
        /// </summary>
        [Property(Required = true, Default = false)]
        [Access(Owner = true, Referrer = true)]
        public bool Pull { get; set; }

        /// <summary>
        /// Email address that is used to send archived email messages to.
        /// </summary>
        [Property(Format = "email")]
        [Access(Owner = true, Referrer = false)]
        public string JournalAddress { get; set; }

        [Access(Owner = true, Referrer = true)]
        public Credentials ArchiveAccessCredentials { get; set; }

        [Access(Owner = true, Referrer = true)]
        public AccessEndpoints ArchiveAccessEndpoint { get; set; }

        [Relation(Required = false, Collection = true)]
        public Mailbox Archivings { get; set; }
    }

    public struct AccessEndpoints
    {
        public List<Socket> Pop3 { get; set; }
        public List<Socket> Imap4 { get; set; }
    }
}
