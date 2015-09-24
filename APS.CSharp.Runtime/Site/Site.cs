using System;
using System.Reflection;
using System.Web;

namespace APS.CSharp.Runtime.Site
{
    internal class Site
    {
        /// <summary>
        /// Generates and returns the informations from the current runtime
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The complete text for the HTML page</returns>
        internal static string GetInfos(HttpRequest request)
        {
            // retrieves the page from the DLL
            string pageText = Helper.GetEmbeddedResource("APS.CSharp.Runtime.Site.infos.html");
            // Set's the version of the Runtime
            pageText = pageText.Replace("{version}", Assembly.GetExecutingAssembly().GetName().Version.ToString());

            // Returning the current controller associated with the endpoint currently beeing called
            Internal.Controller currentController = Controllers.CurrentController(request);
            if (currentController != null)
                pageText = pageText.Replace("{currentController}", currentController.FullName);
            else
                pageText = pageText.Replace("{currentController}", "<font color='red'>There is no current controller associated with current path.</font>");

            // Returns all the endpoints installed in the system, with more detailed information
            string endpoints = "";
            foreach (Internal.Controller instCont in Controllers.GetControllers())
            {
                endpoints += (instCont.Endpoint == null ? "<font color=red><i>Missing endpoint: </i>" : "") +
                  instCont.FullName + " / endpoint: " + instCont.Endpoint + "<br />" +
                  (instCont.Endpoint == null ? "</font>" : "");

                foreach (string type in Controllers.GetApplications(instCont.Assembly))
                    endpoints += "&nbsp&nbsp&nbsp&nbsp> " + type + "<br />";
            }
            pageText = pageText.Replace("{endpoints}", endpoints);

            return pageText;
        }

        /// <summary>
        /// Generates the page to view the APSC requests made to the runtime
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The complete text of the html page</returns>
        internal static string GetAPSRequests(HttpRequest request)
        {
            string requestLine = "<tr><td><a href='/.apsrequests?id={0}'>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>";
            string pageText = "";
            if (string.IsNullOrEmpty(request.QueryString["id"]))
            {
                // retrieves the page from the DLL
                pageText = Helper.GetEmbeddedResource("APS.CSharp.Runtime.Site.apsrequests.html");
                // Set's the version of the Runtime
                pageText = pageText.Replace("{version}", Assembly.GetExecutingAssembly().GetName().Version.ToString());

                string response = "";
                foreach (Database.APSRequests apsRequest in DataAccess.APSRequestsGet())
                    response += string.Format(requestLine, apsRequest.Id, apsRequest.Date.ToShortTimeString(),
                        apsRequest.HttpMethod, apsRequest.ControllerUri, apsRequest.Url, apsRequest.InstanceId, apsRequest.TransationId);

                pageText = pageText.Replace("{apsRequest}", response);
            }
            else
            {
                pageText = Helper.GetEmbeddedResource("APS.CSharp.Runtime.Site.apsrequest.html");

                var apsRequest = DataAccess.APSRequestsGet(int.Parse(request.QueryString["id"]));
                pageText = pageText.Replace("{id}", apsRequest.Id.ToString()).
                                Replace("{date}", apsRequest.Date.ToShortTimeString()).
                                Replace("{httpMethod}", apsRequest.HttpMethod).
                                Replace("{controllerUri}", apsRequest.ControllerUri).
                                Replace("{url}", apsRequest.Url).
                                Replace("{identity}", apsRequest.Identity).
                                Replace("{instanceId}", apsRequest.InstanceId).
                                Replace("{requestPhase}", apsRequest.RequestPhase).
                                Replace("{transationId}", apsRequest.TransationId).
                                Replace("{content}", apsRequest.Content);
            }
            return pageText;
        }

        /// <summary>
        /// Generates the page to view the APSC request made to the runtime
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The complete text of the html page</returns>
        internal static string GetRequest(HttpRequest request)
        {
            string requestLine = "<tr><td><a href='/.requests?id={0}'>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>";
            string pageText = "";
            if (string.IsNullOrEmpty(request.QueryString["id"]))
            {
                // retrieves the page from the DLL
                pageText = Helper.GetEmbeddedResource("APS.CSharp.Runtime.Site.requests.html");
                // Set's the version of the Runtime
                pageText = pageText.Replace("{version}", Assembly.GetExecutingAssembly().GetName().Version.ToString());

                string response = "";
                foreach (Database.Request dbRequest in DataAccess.RequestsGet())
                    response += string.Format(requestLine, dbRequest.Id, dbRequest.Date.ToShortTimeString(),
                        dbRequest.Url, dbRequest.HttpMethod, dbRequest.ContentLenght, dbRequest.UserHostname, dbRequest.UserHostAddress);

                pageText = pageText.Replace("{requests}", response);
            }
            else
            {
                pageText = Helper.GetEmbeddedResource("APS.CSharp.Runtime.Site.request.html");

                var dbRequest = DataAccess.RequestsGet(int.Parse(request.QueryString["id"]));
                pageText = pageText.Replace("{id}", dbRequest.Id.ToString()).
                                Replace("{date}", dbRequest.Date.ToShortTimeString()).
                                Replace("{url}", dbRequest.Url).
                                Replace("{contentLength}", dbRequest.ContentLenght.ToString()).
                                Replace("{httpMethod}", dbRequest.HttpMethod).
                                Replace("{headers}", string.Join("<br/>", HttpUtility.UrlDecode(dbRequest.Headers).Split('&'))).

                                Replace("{params}", string.Join("<br />", HttpUtility.UrlDecode(dbRequest.Params).Split('&'))).
                                Replace("{serverVariables}", string.Join("<br />", HttpUtility.UrlDecode(dbRequest.ServerVariables).Split('&'))).
                                Replace("{content}", dbRequest.Content).
                                Replace("{userHostname}", dbRequest.UserHostname).
                                Replace("{userHostAddress}", dbRequest.UserHostAddress);
            }
            return pageText;
        }

        /// <summary>
        /// Generates the page to view the bindings of the Endpoints
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The complete text of the html page</returns>
        internal static string GetBindings(HttpRequest request)
        {
            string requestLine = "<tr><td><a href='/.bindings?id={0}'>{0}</td><td>{1}</td><td>{2}</td></tr>";
            string pageText = "";
            if (string.IsNullOrEmpty(request.QueryString["id"]))
            {
                // retrieves the page from the DLL
                pageText = Helper.GetEmbeddedResource("APS.CSharp.Runtime.Site.bindings.html");
                // Set's the version of the Runtime
                pageText = pageText.Replace("{version}", Assembly.GetExecutingAssembly().GetName().Version.ToString());

                string response = "";
                foreach (Database.Bindings binding in DataAccess.BindingGet())
                    response += string.Format(requestLine, binding.InstanceId, binding.APSType,
                        binding.ControllerUri);

                pageText = pageText.Replace("{bindings}", response);
            }
            else
            {
                pageText = Helper.GetEmbeddedResource("APS.CSharp.Runtime.Site.binding.html");

                //var dbRequest = DataAccess.RequestsGet(int.Parse(request.QueryString["id"]));
                //pageText = pageText.Replace("{id}", dbRequest.Id.ToString()).
                //                Replace("{date}", dbRequest.Date.ToShortTimeString()).
                //                Replace("{url}", dbRequest.Url).
                //                Replace("{contentLength}", dbRequest.ContentLenght.ToString()).
                //                Replace("{httpMethod}", dbRequest.HttpMethod).
                //                Replace("{headers}", string.Join("<br/>", HttpUtility.UrlDecode(dbRequest.Headers).Split('&'))).

                //                Replace("{params}", string.Join("<br />", HttpUtility.UrlDecode(dbRequest.Params).Split('&'))).
                //                Replace("{serverVariables}", string.Join("<br />", HttpUtility.UrlDecode(dbRequest.ServerVariables).Split('&'))).
                //                Replace("{content}", dbRequest.Content).
                //                Replace("{userHostname}", dbRequest.UserHostname).
                //                Replace("{userHostAddress}", dbRequest.UserHostAddress);
            }
            return pageText;
        }
    }
}
