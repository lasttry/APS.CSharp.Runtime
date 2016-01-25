using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.Core
{
    /// <summary>
    /// An admin user is the kind of user that has administrative access to the application services.
    /// </summary>
    [ResourceBase(ApsVersion = "2.0", Id = "http://aps-standard.org/types/core/admin-user/1.0")]
    public class AdminUser : User
    {
    }
}
