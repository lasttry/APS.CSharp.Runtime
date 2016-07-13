using APS.CSharp.SDK;
using APS.CSharp.SDK.Attributes;
using APS.CSharp.SDK.Types.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace APS.Controller.MarketGoo
{
    [ResourceBase(ApsVersion = "2.0", Id = "http://odin.com/MarketGoo/application/1.0", Implements = new string[] { "http://odin.com/MarketGoo/application/1.0" })]
    public class applications : Application
    {
        public object aps { get; set; }


        [Operation(Verb = HttpVerbs.GET, Path = "/checkConnection")]
        [Param(Name = "host", Kind = ParamSource.Query, Type = typeof(string))]
        [Param(Name = "xauthtoken", Kind = ParamSource.Query, Type = typeof(string))]
        public int checkConnection(string host, string xauthtoken)
        {
            return 0;
        }

        [Operation(Verb = HttpVerbs.GET, Path = "/getDescription", ResponseContentType = "application/json", ErrorResponseType = "object")]
        [Param(Name = "name", Kind = ParamSource.Query, Type = typeof(string))]
        [Param(Name = "language", Kind = ParamSource.Query, Type = typeof(string))]
        public string getDescription(string name, string language)
        {
            return "Description";
        }

        [Operation(Verb = HttpVerbs.GET, Path = "/retrieveDescriptions", ResponseType = "object", ErrorResponseType = "object", ErrorResponseProperties = "code:integer;error:string;message:string")]
        public string retrieveDescriptions()
        {
            return "";
        }

        [Operation(Verb = HttpVerbs.PUT, Path = "/setDescriptions")]
        [Param(Name= "descriptions", Kind = ParamSource.Body, Type =typeof(string))]
        public object setDescriptions(string descriptions)
        {
            return descriptions;
        }
        
        [Property(Description ="BrandedName", Encrypted = false, Final = false, Format = "uri", Headline = false, MaxItems =0, MaxLength =255, MinItems =0, MinLength = 0, Pattern ="pattern", ReadOnly = true, Required = false, Title = "Branded Name", UniqueItems =false, Unit ="unit")]
        public string BrandedName { get; set; }
        [Property]
        public string Host { get; set; }
        [Property]
        public string XAuthToken { get; set; }

        [Relation]
        public List<product> product { get; set; }
    }
}
