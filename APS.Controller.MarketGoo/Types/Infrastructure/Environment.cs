using APS.CSharp.SDK.Attributes;
using System;
using System.Collections.Generic;


namespace APS.CSharp.Test.Types.Infrastructure
{
    /// <summary>
    /// The base environment type provides an ability to deploy APS applications with certain requirements to their environment. Any type that implements the application type may require an environment type as a dependency through a strong relation.
    /// </summary>
    [ResourceBase(ApsVersion = "2.0", Id = "http://aps-standard.org/types/infrastructure/environment/1.0")]
    public class Environment
    {
        [Property(Description = "Application access point")]
        public string EntryPoint { get; set; }

        [Property(Description = "List of supported engines", Pattern = "php|python|perl|java|.net|exec|.+")]
        public List<String> Engines { get; set; }

        public Hardware Hardware { get; set; }

        public Platform Platform { get; set; }

        [Relation]
        public Core.Application Application { get; set; }
    }
}
