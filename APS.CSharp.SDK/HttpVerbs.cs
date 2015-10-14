using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.SDK
{

    /// <summary>
    /// List of the HttpVerbs as refered in RFC2616
    /// http://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html
    /// </summary>
    public enum HttpVerbs
    {
        // 9.2 OPTIONS
        OPTIONS,
        // 9.3 GET
        GET,
        //9.4 HEAD
        HEAD,
        // 9.5 POST
        POST,
        // 9.6 PUT
        PUT,
        // 9.7 DELETE
        DELETE,
        // 9.8 TRACE
        TRACE,
        // 9.9 CONNECT
        CONNECT
    }
}
