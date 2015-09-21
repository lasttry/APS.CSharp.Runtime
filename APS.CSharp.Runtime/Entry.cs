using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace APS.CSharp.Runtime
{
    public class Entry : IHttpHandler
    {
        bool IHttpHandler.IsReusable { get { return true; } }
        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            Trace.TraceInformation("IHttpHandler.ProcessRequest.STR-Requesting file: {0}", context.Request.PhysicalPath);
            if (File.Exists(context.Request.PhysicalPath))
                
                System.Web.UI.PageParser.
                    GetCompiledPageInstance(context.Request.Path, context.Request.PhysicalPath, context).
                    ProcessRequest(context);
            if (context.Request.Path.EndsWith("favicon.ico"))
                return;
            Trace.TraceInformation("IHttpHandler.ProcessRequest.END");
            Trace.Flush();
            //Logger.Start("Rest.IHttpHandler.ProcessRequest");

            //string documentContents;
            //using (Stream receiveStream = context.Request.InputStream)
            //{
            //    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
            //    {
            //        documentContents = readStream.ReadToEnd();
            //    }
            //}

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
            //#region User Calls .info
            //// This means we need to send the debug information to the browser
            //else if (context.Request.Path.EndsWith(".infos"))
            //{
            //    //context.Response.Write(Logger.GetInfos(context.Request));
            //    //Logger.End("Rest.IHttpHandler.ProcessRequest");
            //    return;
            //}
            //#endregion
            //#region Users Calls .request
            //else if (context.Request.Path.EndsWith(".requests"))
            //{
            //    context.Response.Write(Logger.GetRequest(context.Request));
            //    //Logger.End("Rest.IHttpHandler.ProcessRequest");
            //    return;
            //}
            //#endregion
            //#region APS request (.apsrequests)
            //else if (context.Request.Path.ToLower().EndsWith(".apsrequests"))
            //{
            //    if (context.Request.QueryString.AllKeys.Contains("clearLogs"))
            //    {
            //        DataAccess.ClearAllLogs();
            //        context.Response.Redirect("~/.apsrequests");
            //    }
            //    context.Response.Write(Logger.GetAPSRequest(context.Request));
            //    Logger.End("Rest.IHttpHandler.ProcessRequest");
            //    return;
            //}
            //#endregion
            //Logger.Request(context.Request, documentContents);
            //dynamic contents = null;
            //if (context.Request.ContentType == "application/json" && !string.IsNullOrEmpty(documentContents))
            //{
            //    contents = JObject.Parse(documentContents);

            //    if (contents.aps.x509 != null)
            //    {
            //        DataAccess.SaveCertificate(
            //            (string)context.Request.Headers["APS-Instance-ID"],
            //            (string)contents.aps.type,
            //            (string)context.Request.Headers["APS-Controller-URI"],
            //            (string)contents.aps.x509.self,
            //            (string)contents.aps.x509.controller);
            //    }
            //}
            //Controller.InvokeByRequest(context.Request, documentContents);

            //Logger.End("Rest.IHttpHandler.ProcessRequest");
        }
    }
}
