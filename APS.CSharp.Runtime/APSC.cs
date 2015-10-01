using APS.CSharp.SDK;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Security.Cryptography.X509Certificates;

namespace APS.CSharp.Runtime
{
    public class APSC : IAPSC
    {
        public string InstanceId { get; set; }

        const string APS_CONTROLLER_URI = "APS-Controller-URI";
        const string APS_SESSION = "APS-Transaction-ID";
        const string APS_RESOURCE_ID = "APS-Resource-ID";
        const string APS_REQUEST_ID = "APS-Request-ID";
        const string APS_TOKEN = "APS-Token";

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
            UnprovisionResource(new ResourceBase() { Id = resourceId });
        }

        public void UnregisterResource(ResourceBase resource)
        {

        }
        public void UnregisterResource(string resourceId)
        {
            UnregisterResource(new ResourceBase() { Id = resourceId });
        }
        public void RegisterResource(ResourceBase resource)
        {

        }
        public void RegisterResource(string resourceId)
        {
            RegisterResource(new ResourceBase() { Id = resourceId });
        }
        public void ConfigureResource(ResourceBase resource)
        {

        }
        public void UpdateResource(ResourceBase resource)
        {

        }
        public void ProvisionResource(ResourceBase resource)
        {

        }
        public void UnlinkResource(ResourceBase resource, string relationName, ResourceBase linkResource)
        {

        }
        public void UnlinkResource(ResourceBase resource, string relationName, string linkResource)
        {
            UnlinkResource(resource, relationName, new ResourceBase() { Id = linkResource });
        }
        public void UnlinkResource(string resourceId, string relationName, string linkResource)
        {
            UnlinkResource(new ResourceBase() { Id = resourceId }, relationName, new ResourceBase() { Id = linkResource });
        }
        public void UnlinkResource(string resourceId, string relationName, ResourceBase linkResource)
        {
            UnlinkResource(new ResourceBase() { Id = resourceId }, relationName, linkResource );
        }
        public ResourceBase LinkResource(ResourceBase resource, string relationName, ResourceBase linkResource)
        {
            return null;
        }
        public ResourceBase LinkResource(ResourceBase resource, string relationName, string linkResourceId)
        {
            return LinkResource(resource, relationName, new ResourceBase() { Id = linkResourceId });
        }
        public ResourceBase LinkResource(string resourceId, string relationName, ResourceBase linkResource)
        {
            return LinkResource(new ResourceBase() { Id = resourceId }, relationName, linkResource);
        }
        public ResourceBase LinkResource(string resourceId, string relationName, string linkResourceId)
        {
            return LinkResource(new ResourceBase() { Id = resourceId }, relationName, new ResourceBase() { Id = linkResourceId });
        }

        public ResourceBase GetResource(string id)
        {
            return null;
        }

        public List<ResourceBase> GetResources(string rqlFilter, string path)
        {
            return null;
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
        internal bool SendRequest(string path, out string content)
        {
            content = String.Empty;

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

                HttpResponseMessage response;
                try
                {
                    // send the request to the APSC
                    response = apscclient.GetAsync(path).Result;
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
                    return true;
                }
                else
                    // we have one error, so let's throw it with the actual message
                    throw new APSException((int)response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}
