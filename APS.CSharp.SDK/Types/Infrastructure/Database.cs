using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.SDK.Types.Infrastructure
{
    /// <summary>
    /// A resource of the “Database” type represents an interface for a set of database tables and a user with administrative privileges to these tables. The resource must contain the name of a database instance with tables, login/password of the user and host/port which are used to connect to the database.
    /// A database may contain a table prefix.In this case creation of tables which name does not include the prefix must be prohibited to the administrative user.Otherwise the user must be allowed to create and manage tables with any name.
    /// Different resources of the “Database” type may have the same name of database (may share the same database instance).
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/infrastructure/database/1.0")]
    public class Database
    {
        /// <summary>
        /// IP address or hostname of the server on which the database is available.
        /// </summary>
        [Property(Format ="host-name", Required = true)]
        public string Host { get; set; }

        /// <summary>
        /// Port of the server on which the database is available.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Login of an administrative user to the tables.
        /// </summary>
        [Property(Required = true)]
        public string Login { get; set; }

        /// <summary>
        /// Password of the administrative user to the tables.
        /// </summary>
        [Property(Required = true, Encrypted = true)]
        [Access(Referrer = true)]
        public string Password { get; set; }

        /// <summary>
        /// Name of the database which contains the tables.
        /// </summary>
        [Property(Required = true)]
        public string Name { get; set; }

        /// <summary>
        /// Prefix for the tables.
        /// </summary>
        public string TablePrefix { get; set; }

        /// <summary>
        /// String with a User DSN for the database with tables, see http://en.wikipedia.org/wiki/Data_Source_Name. It must be in sync with the server, login, password, name properties.
        /// For example:
        /// mysql://user31:o1Kj@192.168.0.1:3306/user31_db
        /// </summary>
        public string UserDSN { get; set; }

        /// <summary>
        /// String with a System DSN for the database with tables, see http://en.wikipedia.org/wiki/Data_Source_Name. It must be in sync with the server and name properties.
        /// For example:
        /// mysql:host=192.168.0.1;dbname=user31_db
        /// </summary>
        public string SystemDSN { get; set; }

        /// <summary>
        /// Version of the database server where the tables are hosted.
        /// </summary>
        public string Version { get; set; }
    }
}
