using APS.CSharp.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace APS.CSharp.SDK.Types.Web
{
    /// <summary>
    /// A resource of the “Website” type represents a folder with files that is accessible over HTTP protocol on a domain.
    /// The folder requires at least one IP address (“IPAddress” type), which may be shared with other websites.
    /// The domain is not shared between websites. The files and folders of the website may be accessible over FTP/SSH to several users.
    /// The resource may provide access to the files and folders over HTTPS on both the domain and IP address.In this case the “Website” resource must not share its IP address with other websites.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/web/website/1.0")]
    public class Website : Core.Resource
    {
        [Property(Required = true)]
        public string Path { get; set; }

        [Property(Required = true, MinItems = 1)]
        public List<string> Urls { get; set; }

        [Relation(Collection = true)]
        public List<Infrastructure.IPAddress> IpAddress { get; set; }

        [Relation]
        public DNS.Domain Domain { get; set; }

        [Relation]
        public SSL SslCertificate { get; set; }

        /// <summary>
        /// Get a list of all SSH or FTP users.
        /// </summary>
        /// <param name="protocol"></param>
        /// <returns></returns>
        [Operation(Verb = HttpVerbs.GET, Path = "/users/{protocol}")]
        [Param(Kind = ParamSource.Path)]
        [Access(Referrer = true)]
        public List<string> GetUsers(string protocol)
        {
            string result = APSCUtility.APSC.SendRequest(APSCPaths.BuildResourcePath(APSCUtility.APSC.InstanceId, "users", HttpUtility.UrlEncode(protocol)), HttpMethod.Get);

            return APSCUtility.APSC.ConvertJson2Object<List<string>>(result);
        }

        /// <summary>
        /// Add either an SSH or FTP user with the specified login and password. The password must be generated unless specified directly.
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Operation(Verb = HttpVerbs.POST, Path = "/users/{protocol}")]
        [Param(Kind = ParamSource.Path, Name = "protocol")]
        [Param(Kind = ParamSource.Body, Name = "User")]
        public User AddUser(string protocol, User user)
        {
            string result = APSCUtility.APSC.SendRequest(APSCPaths.BuildResourcePath(APSCUtility.APSC.InstanceId, "users", HttpUtility.UrlEncode(protocol)), HttpMethod.Post,
                APSCUtility.APSC.ConvertObject2Json(user));

            return APSCUtility.APSC.ConvertJson2Object<User>(result);
        }

        /// <summary>
        /// The service can return in the response a normalized user name and it may differ from the original username.
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="name"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Operation(Verb = HttpVerbs.PUT, Path = "/users/{protocol}/{name}")]
        [Access(Referrer = true)]
        [Param(Kind = ParamSource.Path, Name = "protocol")]
        [Param(Kind = ParamSource.Path, Name = "name")]
        [Param(Kind = ParamSource.Body, Name = "user")]
        public string SetUser(string protocol, string name, User user)
        {
            string result = APSCUtility.APSC.SendRequest(APSCPaths.BuildResourcePath(APSCUtility.APSC.InstanceId, "users", HttpUtility.UrlEncode(protocol), HttpUtility.UrlEncode(name)), HttpMethod.Put,
               APSCUtility.APSC.ConvertObject2Json(user));

            return result;
        }

        /// <summary>
        /// Delete either an SSH or FTP user with the specified login.
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [Operation(Verb = HttpVerbs.DELETE, Path = "/users/{protocol}/{name}")]
        [Access(Referrer = true)]
        [Param(Kind = ParamSource.Path, Name = "protocol")]
        [Param(Kind = ParamSource.Path, Name = "name")]
        public string DeleteUser(string protocol, string name)
        {
            string result = APSCUtility.APSC.SendRequest(APSCPaths.BuildResourcePath(APSCUtility.APSC.InstanceId, "users", HttpUtility.UrlEncode(protocol), HttpUtility.UrlEncode(name)), HttpMethod.Delete);

            return result;
        }
    }
}
