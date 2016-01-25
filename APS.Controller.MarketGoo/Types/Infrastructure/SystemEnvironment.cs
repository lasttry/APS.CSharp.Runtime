using APS.CSharp.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.Test.Types.Infrastructure
{
    /// <summary>
    /// The SystemEnvironment type implements the base environment. It was designed for deploying applications on top of a bare operating system.
    /// </summary>
    [ResourceBase(ApsVersion = "2.0", Id = "http://aps-standard.org/types/infrastructure/environment/system/1.0")]
    public class SystemEnvironment : Environment
    {
    }
}
