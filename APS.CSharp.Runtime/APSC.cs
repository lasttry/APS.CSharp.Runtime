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
using System.Reflection;

namespace APS.CSharp.Runtime
{
    public class APSC : IAPSC
    {
        public APSC(HttpRequest request)
        {
            if (request == null)
                _request = HttpContext.Current.Request;
            else
                _request = request;

            // If the Instance ID header is not present throw exception.
            if (string.IsNullOrEmpty(Request.Headers[APSCHeaders.APSInstanceId]))
                throw new APSException(500, "The header {0} is not present in the request. Unable to retrieve the instance id.", APSCHeaders.APSInstanceId);

            InstanceId = Request.Headers[APSCHeaders.APSInstanceId];
        }
        private HttpRequest _request;
        private HttpRequest Request { get { return _request; } }

        internal int Timeout = 300;

        public string InstanceId { get; set; }

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
            //SendRequest()
        }
        public T UpdateResource<T>(T resource)
        {
            // we need to get the property of the APS since we are working with T object.
            PropertyInfo apsProperty = resource.GetType().GetProperty("APS");
            if (apsProperty == null)
                // Doesn't contain the APS part of the object, so it doesn't implemnete the resourcebase object.
                throw new Exception("Object resource doesn't contain property APS. is it a ResourceBase object?");
            SDK.APS aps = apsProperty.GetValue(resource) as APS.CSharp.SDK.APS;
            string id = aps.Id;
            // Get the type name so we can use in the path, no need to use 
            string service = resource.GetType().Name;

            string path = APSCPaths.BuildApplicationPath(service, id);

            string updatedResource = SendRequest(path, HttpMethod.Put, Helper.Resource2Json(resource));

            return JsonConvert.DeserializeObject<T>(updatedResource);
        }
        public T ProvisionResource<T>(T resource)
        {
            string createdResource = SendRequest(APSCPaths.ResourcePath, HttpMethod.Post, Helper.Resource2Json(resource));

            return JsonConvert.DeserializeObject<T>(createdResource);
        }

        public void UnlinkResource(ResourceBase resource, string relationName, ResourceBase linkResource)
        {
            string linkPath = APSCPaths.BuildResourcePath(APSCPaths.ResourcePath, resource.APS.Id, relationName, linkResource.APS.Id);
            
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

            return JsonConvert.DeserializeObject<T>(SendRequest(APSCPaths.BuildResourcePath(resource.APS.Id, relationName), HttpMethod.Post, post));
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
            return LinkResource(
                new ResourceBase() {
                    APS = new SDK.APS { Id = resourceId } }, 
                relationName, 
                new ResourceBase() {
                    APS = new SDK.APS { Id = linkResourceId } });
        }

        public ResourceBase GetResource(string id)  
        {
            return GetResource<ResourceBase>(id);
        }

        public T GetResource<T>(string id)
        {
            string resource = "";

            resource = SendRequest(APSCPaths.BuildResourcePath(id), HttpMethod.Get);

            return JsonConvert.DeserializeObject<T>(resource);
        }

        public object GetResources(string rqlFilter, string path)
        {
            if (String.IsNullOrEmpty(path))
                path = APSCPaths.ResourcePath;
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
        public string SendRequest(string path, HttpMethod method, object postContent = null, string contentType = "application/json", string impersonate = null)
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
                    
                    apscRequest.Headers.Add(APSCHeaders.APSInstanceId, Binding.InstanceId);
                    apscRequest.Headers.Add(APSCHeaders.APSControllerUri, Request.Url.Host);
                    
                    apscRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

                    // this should only be sent if we have some content
                    if(method != HttpMethod.Post || method != HttpMethod.Put)
                    {
                        if (postContent.GetType() == typeof(string))
                            apscRequest.Content = new StringContent((string)postContent);
                        else if(postContent.GetType() == typeof(List<KeyValuePair<string, string>>))
                            apscRequest.Content = new FormUrlEncodedContent((List<KeyValuePair<string, string>>)postContent);

                        apscRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    }

                    //GetSession
                    if (Request.Headers.Get(APSCHeaders.APSTransactionId) != null)
                        apscRequest.Headers.Add(APSCHeaders.APSSession, Request.Headers.Get(APSCHeaders.APSTransactionId));
                    //GetToken
                    //TODO: Check what is token and apply it
                    //if ()
                    //    apscRequest.Headers.Add(APS_TOKEN, );

                    //apscRequest.Method = method;

                    // if the impersonate object has information we need to impersonate
                    // for that we send the APS-Resource-ID header with the id of the resource we want to impersonate
                    if(!string.IsNullOrEmpty(impersonate))
                        apscRequest.Headers.Add(APSCHeaders.APSResourceId, impersonate);

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

        public string ConvertObject2Json(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T ConvertJson2Object<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
