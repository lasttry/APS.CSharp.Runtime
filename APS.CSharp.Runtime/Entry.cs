using APS.CSharp.Runtime.Internal;
using APS.CSharp.SDK;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace APS.CSharp.Runtime
{
    public class Entry : IHttpHandler
    {
        bool IHttpHandler.IsReusable { get { return true; } }
        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            string path = context.Request.Path;

            if (path.EndsWith("favicon.ico"))
                return;

            Trace.TraceInformation("Entry.ProcessRequest.STR-Requesting file: {0}", path);

            // this means it's internal resource that we should provide
            if (path.StartsWith("/.Site/"))
            {
                string name = Path.GetFileName(path);

                if (path.Contains("CSS"))
                    context.Response.ContentType = "text/css";

                context.Response.Write(Helper.GetEmbeddedResource("APS.CSharp.Runtime.Site." + name));
                context.Response.End();
                Trace.TraceInformation("Entry.ProcessRequest.END");
                return;
            }

            if (File.Exists(context.Request.PhysicalPath))
            {
                // File exists in the file system, so everything else is ignored.
                Trace.TraceWarning("Entry.ProcessRequest.DGB-File exist in the filesystem, if you think this should not be true, please remove from filesystem or from your Dll the specific file: '{0}'", context.Request.FilePath);
                System.Web.UI.PageParser.
                    GetCompiledPageInstance(path, context.Request.PhysicalPath, context).
                    ProcessRequest(context);
                Trace.TraceInformation("Entry.ProcessRequest.END");
                return;
            }

            //Reading request contents and saving it for later usage
            Trace.TraceInformation("IHttpHandler.ProcessRequest.DGB-Reading Request contents");
            string documentContents;
            using (Stream receiveStream = context.Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    documentContents = readStream.ReadToEnd();
                }
            }
            Trace.TraceInformation("IHttpHandler.ProcessRequest.DGB-Request contents received");
            
            //List<string> headers = new List<string>();
            //foreach (string key in context.Request.Headers.AllKeys)
            //    headers.Add(key + "=" + context.Request.Headers[key]);

            //#region .css
            //if (context.Request.Path.EndsWith("/tables.css"))
            //{
            //    context.Response.ContentType = "text/css";
            //    context.Response.Write(Helper.GetEmbeddedResource("APS.CSharp.Runtime.html.tables.css"));
            //    context.Response.Flush();
            //    return;
            //}
            //#endregion
            #region User Calls .infos
            // This means we need to send the debug information to the browser
            if (path.EndsWith(".infos"))
            {
                Trace.TraceInformation("Entry.ProcessRequest.DGB-Getting .infos page");
                context.Response.Write(Site.Site.GetInfos(context.Request));
                Trace.TraceInformation("Entry.ProcessRequest.END");
                return;
            }
            #endregion
            #region Users Calls .request
            else if (path.EndsWith(".requests"))
            {
                Trace.TraceInformation("Entry.ProcessRequest.DGB-Getting .requests page");
                context.Response.Write(Site.Site.GetRequest(context.Request));
                Trace.TraceInformation("Entry.ProcessRequest.END");
                return;
            }
            #endregion
            #region APS request (.apsrequests)
            else if (path.EndsWith(".apsrequests"))
            {
                Trace.TraceInformation("Entry.ProcessRequest.DGB-Getting .apsrequests page");
                context.Response.Write(Site.Site.GetAPSRequests(context.Request));
                //if (context.Request.QueryString.AllKeys.Contains("clearLogs"))
                //{
                //    DataAccess.ClearAllLogs();
                //    context.Response.Redirect("~/.apsrequests");
                //}
                //context.Response.Write(Logger.GetAPSRequest(context.Request));
                //Logger.End("Rest.IHttpHandler.ProcessRequest");
                Trace.TraceInformation("Entry.ProcessRequest.END");
                return;
            }
            else if (path.EndsWith(".bindings"))
            {
                Trace.TraceInformation("Entry.ProcessRequest.DGB-Getting .apsrequests page");
                context.Response.Write(Site.Site.GetBindings(context.Request));
                Trace.TraceInformation("Entry.ProcessRequest.END");
                return;
            }
            #endregion
            Helper.LogRequest(context.Request, documentContents);
            dynamic contents = null;
            if (context.Request.ContentType == "application/json" && !string.IsNullOrEmpty(documentContents))
            {
                contents = JObject.Parse(documentContents);

                if (contents.aps.x509 != null)
                {
                    DataAccess.BindingAdd(
                        (string)context.Request.Headers["APS-Instance-ID"],
                        (string)contents.aps.type,
                        (string)context.Request.Headers["APS-Controller-URI"],
                        (string)contents.aps.x509.self,
                        (string)contents.aps.x509.controller);
                }
            }

            APSException errorDetails = null;

            object invokeResult = Controllers.ProcessIncomingRequest(context.Request, documentContents, out errorDetails);

            // Means that we have nothing to return and that the result was false, the result is only bool if no return to APSC
            if (invokeResult.GetType() == typeof(bool))
            {
                //context.Response.SuppressContent = true;
                if (!(bool)invokeResult)
                {
                    Trace.TraceWarning("IHttpHandler.ProcessRequest.DGB-If we reached this far, means we have nothing to serve.");
                    Trace.TraceError(JsonConvert.SerializeObject(errorDetails));
                    context.Response.Write(JsonConvert.SerializeObject(new { code = errorDetails.code, message = errorDetails.message }));
                    context.Response.StatusCode = errorDetails.code;
                    context.Response.End();
                    Trace.TraceInformation("IHttpHandler.ProcessRequest.END");
                    return;
                }
                    
            }
            else
            {
                context.Response.Write(invokeResult);
            }
            // if we reach here then we have something to return to the APSC
            context.Response.StatusCode = 200;
            context.Response.End();

        }
    }
}
