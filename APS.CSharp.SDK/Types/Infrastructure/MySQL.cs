using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.SDK.Types.Infrastructure
{
    /// <summary>
    /// A resource of the “MySQL” type represents a set of MySQL tables with an administrative user for them. Its getDSN() method must contain a DSN with ‘mysql’ driver name.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/infrastructure/database/mysql/1.0")]
    public class MySQL : Database
    {
    }
}
