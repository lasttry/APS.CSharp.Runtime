using APS.CSharp.SDK.Attributes;
using System.Collections.Generic;
using System.Net.Http;

namespace APS.CSharp.SDK.Types.Core
{
    /// <summary>
    /// Each application declares a resource of the “Application” type. APS controller creates a resource of this time during provisioning of an application instance. So, such a resource presents an instance of the application.
    /// If an application instance must be installed in a certain environment, the resource may require one of environment types.In this case, for every new application instance, a new environment has to be created.
       /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/core/application/1.0")]
    public class Application : Resource
    {

        [Operation(Verb = HttpVerbs.POST, Path = "/upgrade")]
        [Param(Kind = ParamSource.Query)]
        public object Upgrade(string version)
        {
            object result = APSCUtility.APSC.SendRequest(APSCPaths.BuildResourcePath(APSCUtility.APSC.InstanceId, "/upgrade"), 
                HttpMethod.Post, 
                APSCUtility.GetPostData("version", version), 
                "multipart/form-data");

            return result;
        }

    }
}
