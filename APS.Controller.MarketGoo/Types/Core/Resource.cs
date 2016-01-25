using APS.CSharp.SDK;
using APS.CSharp.SDK.Attributes;
using System;
using System.Net.Http;

namespace APS.CSharp.Test.Types.Core
{
    /// <summary>
    /// The Resource type is the base type for any resource operations.
    /// </summary>
    [ResourceBase("http://aps-standard.org/types/core/resource/1.0")]
    public class Resource
    {
        [Operation(Name = "provision", Verb = HttpVerbs.POST, Path = "/", Static = true)]
        [Access(Admin = true, Owner = true, Referrer = false)]
        public string Provision()
        {
            return null;
        }

        [Operation(Name = "retrieve", Verb = HttpVerbs.GET, Path = "/")]
        [Access(Admin = true, Owner = true, Referrer = false)]
        public string Retrieve()
        {
            return null;
        }

        [Operation(Name = "configure", Verb = HttpVerbs.PUT, Path = "/")]
        [Access(Admin = true, Owner = true, Referrer = false)]
        [Param(Name = "n", Kind = ParamSource.Body, Type = typeof(Resource))]
        public string Configure<Resource>(Resource n)
        {
            return null;
        }

        [Operation(Name = "unprovision", Verb = HttpVerbs.DELETE, Path = "/")]
        [Access(Admin = true, Owner = true, Referrer = false)]
        public string Unprovision()
        {
            return null;
        }

        public Counter Counter { get; set; }
        public Limit Limit { get; set; }
        public Usage Usage { get; set; }
        public NotificationSource NotificationSource { get; set; }
        public Notification Notification { get; set; }
    }
}
