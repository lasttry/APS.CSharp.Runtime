using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.Core
{
    /// <summary>
    /// The User Service type represents a generic service allocated to a user resource through the user relation.
    /// Resources that are intended for service users, should implement this type.
    /// </summary>
    [ResourceBase(ApsVersion = "2.0", Id = "http://aps-standard.org/types/core/user/service/1.0")]
    public class UserService : Resource
    {
        [Relation]
        public User User { get; set; }
    }
}
