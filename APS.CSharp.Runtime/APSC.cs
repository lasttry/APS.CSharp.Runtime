using APS.CSharp.SDK;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace APS.CSharp.Runtime
{
    public class APSC : IAPSC
    {
        public APSC(HttpRequest request)
        {
            _request = request;
        }
        private HttpRequest _request;
        private HttpRequest Request { get { return _request; } }

        internal int Timeout = 300;

        public string InstanceId { get; set; }

        const string APS_CONTROLLER_URI = "APS-Controller-URI";
        const string APS_SESSION = "APS-Transaction-ID";
        const string APS_RESOURCE_ID = "APS-Resource-ID";
        const string APS_REQUEST_ID = "APS-Request-ID";
        const string APS_TOKEN = "APS-Token";
        const string APS_INSTANCE_ID = "APS-Instance-ID";
        const string APS_TRANSACTION_ID = "APS-Transaction-ID";

        // needs to start with / or the virtualpathutility will throw exception
        const string APSC_RESOURCEPATH = "/aps/2/resources/";

        public void Subscriptions(ResourceBase resource)
        {
        }

        public void Unsubscribe(ResourceBase resource)
        {

        }
        public void Subscribe(ResourceBase resource)
        {

        }
        public void UnprovisionResource(ResourceBase resource)
        {

        }
        public void UnprovisionResource(string resourceId)
        {
            UnprovisionResource(new ResourceBase() { APS = new SDK.APS { Id = resourceId } });
        }

        public void UnregisterResource(ResourceBase resource)
        {

        }
        public void UnregisterResource(string resourceId)
        {
            UnregisterResource(new ResourceBase() { APS = new SDK.APS { Id = resourceId } });
        }
        public void RegisterResource(ResourceBase resource)
        {

        }
        public void RegisterResource(string resourceId)
        {
            RegisterResource(new ResourceBase() { APS = new SDK.APS { Id = resourceId } });
        }
        public void ConfigureResource(ResourceBase resource)
        {

        }
        public void UpdateResource(ResourceBase resource)
        {

        }
        public void ProvisionResource(ResourceBase resource)
        {
            string path = APSC_RESOURCEPATH;

            SendRequest(path, HttpMethod.Post, Helper.Resource2Json(resource));
        }

        public void UnlinkResource(ResourceBase resource, string relationName, ResourceBase linkResource)
        {
            string linkPath = Helper.CombinePath(APSC_RESOURCEPATH, resource.APS.Id, relationName, linkResource.APS.Id);
            
            SendRequest(linkPath, HttpMethod.Delete);
        }
        public void UnlinkResource(ResourceBase resource, string relationName, string linkResource)
        {
            UnlinkResource(resource, relationName, new ResourceBase() { APS = new SDK.APS { Id = linkResource } });
        }
        public void UnlinkResource(string resourceId, string relationName, string linkResource)
        {
            UnlinkResource(new ResourceBase() { APS = new SDK.APS { Id = resourceId } }, relationName, new ResourceBase() { APS = new SDK.APS { Id = linkResource } });
        }
        public void UnlinkResource(string resourceId, string relationName, ResourceBase linkResource)
        {
            UnlinkResource(new ResourceBase() { APS = new SDK.APS { Id = resourceId } }, relationName, linkResource );
        }
        public T LinkResource<T>(ResourceBase resource, string relationName, ResourceBase linkResource)
        {
            string post = JsonConvert.SerializeObject(new { aps = new { id = linkResource.APS.Id } });

            return JsonConvert.DeserializeObject<T>(SendRequest(VirtualPathUtility.Combine(VirtualPathUtility.Combine(APSC_RESOURCEPATH, resource.APS.Id), relationName), HttpMethod.Post, post));
        }
        public ResourceBase LinkResource(ResourceBase resource, string relationName, ResourceBase linkResource)
        {
            return LinkResource<ResourceBase>(resource, relationName, linkResource);
        }
        public ResourceBase LinkResource(ResourceBase resource, string relationName, string linkResourceId)
        {
            return LinkResource(resource, relationName, new ResourceBase() { APS = new SDK.APS { Id = linkResourceId } });
        }
        public ResourceBase LinkResource(string resourceId, string relationName, ResourceBase linkResource)
        {
            return LinkResource(new ResourceBase() { APS = new SDK.APS { Id = resourceId } }, relationName, linkResource);
        }
        public ResourceBase LinkResource(string resourceId, string relationName, string linkResourceId)
        {
            return LinkResource(new ResourceBase() { APS = new SDK.APS { Id = resourceId } }, relationName, new ResourceBase() { APS = new SDK.APS { Id = linkResourceId } });
        }

        public ResourceBase GetResource(string id)  
        {
            return GetResource<ResourceBase>(id);
        }

        public T GetResource<T>(string id)
        {
            string resource = "";

            resource = SendRequest(VirtualPathUtility.Combine(APSC_RESOURCEPATH, id), HttpMethod.Get);

            return JsonConvert.DeserializeObject<T>(resource);
        }

        public object GetResources(string rqlFilter, string path)
        {
            if (String.IsNullOrEmpty(path))
                path = APSC_RESOURCEPATH;
            string resources = "";
            resources = SendRequest(path, HttpMethod.Get);
                //throw new Exception("Failed to retrieve the resources from the APSC.");

            return JsonConvert.DeserializeObject(resources);
        }

        internal Database.Bindings _binding;
        internal Database.Bindings Binding
        {
            get
            {
                if (_binding == null)
                    _binding = DataAccess.BindingGet(InstanceId);
                return _binding;
            }
        }


        /// <summary>
        /// Sends the request to the APSC
        /// </summary>
        /// <param name="path">The path to use</param>
        /// <param name="content">The result content</param>
        /// <returns>true if request is success</returns>
        internal string SendRequest(string path, HttpMethod method, string postContent = "", string contentType = "application/json")
        {
            string content = String.Empty;

            // Let's configure the WebRequestHandler to understand the certificate handshake.
            var clientHandler = new WebRequestHandler();
            clientHandler.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequired;
            clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;

            // We add the aps client certificate to the clientHanlder
            clientHandler.ClientCertificates.Add(Common.Certificates.GetFullCertificate(System.Text.Encoding.UTF8.GetBytes(Binding.CertificateSelf)));

            using (var apscclient = new HttpClient(clientHandler))
            {
                // it will ignore the apsc certificate error.
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                // Setting the APSC Uri
                apscclient.BaseAddress = new Uri(Binding.ControllerUri);
                apscclient.DefaultRequestHeaders.Accept.Clear();
                apscclient.Timeout = new TimeSpan(0,0,Timeout);

                HttpResponseMessage response;
                try
                {

                    HttpRequestMessage apscRequest = new HttpRequestMessage(method, path);
                    
                    apscRequest.Headers.Add(APS_INSTANCE_ID, Binding.InstanceId);
                    apscRequest.Headers.Add(APS_CONTROLLER_URI, Request.Url.Host);
                    
                    apscRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

                    // this should only be sent if we have some content
                    if(method != HttpMethod.Post || method != HttpMethod.Put)
                    {
                        apscRequest.Content = new StringContent(postContent);
                        apscRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

                    }

                    //GetSession
                    if (Request.Headers.Get(APS_TRANSACTION_ID) != null)
                        apscRequest.Headers.Add(APS_SESSION, Request.Headers.Get(APS_TRANSACTION_ID));
                    //GetToken
                    //TODO: Check what is token and apply it
                    //if ()
                    //    apscRequest.Headers.Add(APS_TOKEN, );
                        
                    //apscRequest.Method = method;

                    // send the request to the APSC
                    response = apscclient.SendAsync(apscRequest).Result;
                }
                catch(Exception e)
                {
                    // allow to return the actual error message, instead of the generic one
                    while (e.InnerException != null)
                        e = e.InnerException; 
                    throw new Exception(e.Message);
                }
                if (response.IsSuccessStatusCode)
                {
                    // we have a good response (200 OK) let's read the content
                    content = response.Content.ReadAsStringAsync().Result;
                    
                }
                else
                    // we have one error, so let's throw it with the actual message
                    throw new APSException((int)response.StatusCode, response.ReasonPhrase);
            }
            return content;
        }
    }
}
