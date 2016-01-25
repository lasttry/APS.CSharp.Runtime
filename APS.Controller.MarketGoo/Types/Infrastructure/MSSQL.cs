using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.Infrastructure
{
    /// <summary>
    /// A resource of the “MSSQL” type represents a set of Microsoft SQL tables with an administrative user for them. Its getDSN() method must contain a DSN with ‘microsoft:sqlserver’ driver name.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/infrastructure/database/mssql/1.0")]
    public class MSSQL : Database
    {
    }
}
