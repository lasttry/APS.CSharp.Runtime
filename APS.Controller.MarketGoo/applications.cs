using APS.CSharp.SDK;
using APS.CSharp.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace APS.Controller.MarketGoo
{
    [ResourceBase(Id = "http://odin.com/MarketGoo/application/1.0")]
    public class applications : APS.CSharp.SDK.Application
    {
        [Operation(Verb = HttpVerbs.GET, Path = "/checkConnection")]
        [Param(Name = "host", Source = ParamSource.Query, Type = typeof(string))]
        [Param(Name = "xauthtoken", Source = ParamSource.Query, Type = typeof(string))]
        public int checkConnection(string host, string xauthtoken)
        {
            return 0;
        }

        [Operation(Verb = HttpVerbs.GET, Path = "/getDescription")]
        [Param(Name = "name", Source = ParamSource.Query, Type = typeof(string))]
        [Param(Name = "language", Source = ParamSource.Query, Type = typeof(string))]
        public string getDescription(string name, string language)
        {
            return "Description";
        }

        [Operation(Verb = HttpVerbs.GET, Path = "/retrieveDescriptions")]
        public string retrieveDescriptions()
        {
            return "";
        }

        [Operation(Verb = HttpVerbs.PUT, Path = "/setDescriptions")]
        [Param(Name= "descriptions", Source =ParamSource.Body, Type =typeof(string))]
        public object setDescriptions(string descriptions)
        {
            return descriptions;
        }
        
        public string BrandedName { get; set; }
        public string Host { get; set; }
        public string XAuthToken { get; set; }

        [Link]
        public List<product> product { get; set; }
    }
}
