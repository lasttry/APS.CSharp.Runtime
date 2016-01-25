using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.Core
{
    /// <summary>
    /// A service user is the kind of user that is attached to some service as an end user.
    /// </summary>
    [ResourceBase(ApsVersion = "2.0", Id = "http://aps-standard.org/types/core/service-user/1.0")]
    public class ServiceUser : User
    {
    }
}
