using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.SDK
{
    public interface IAPSC
    {

        string InstanceId { get; set; }

        /// <summary>
        /// Retrives all the subscriptions of the specified resource
        /// </summary>
        /// <param name="resource"></param>
        void Subscriptions(ResourceBase resource);

        /// <summary>
        /// Unsubscribe the specified resource
        /// </summary>
        /// <param name="resource"></param>
        void Unsubscribe(ResourceBase resource);

        /// <summary>
        /// Subscribe the specified resource
        /// </summary>
        /// <param name="resource"></param>
        void Subscribe(ResourceBase resource);

        /// <summary>
        /// When there is a need to remove a resource, a REST DELETE request should be sent to the /aps/2/resources/{ID} URI of the APS controller, where {ID} is the resource ID.
        /// https://doc.apsstandard.org/2.1/spec/api/resources/delete/
        /// </summary>
        /// <param name="resource"></param>
        void UnprovisionResource(ResourceBase resource);
        /// <summary>
        /// When there is a need to remove a resource, a REST DELETE request should be sent to the /aps/2/resources/{ID} URI of the APS controller, where {ID} is the resource ID.
        /// https://doc.apsstandard.org/2.1/spec/api/resources/delete/
        /// </summary>
        /// <param name="resource"></param>
        void UnprovisionResource(string resourceId);

        /// <summary>
        /// deletes the specified resource from the APS controller
        /// https://doc.apsstandard.org/2.1/spec/api/application/instance-operations/#spec-api-application-resource-delete
        /// </summary>
        /// <param name="resource"></param>
        void UnregisterResource(ResourceBase resource);
        /// <summary>
        /// deletes the specified resource from the APS controller
        /// https://doc.apsstandard.org/2.1/spec/api/application/instance-operations/#spec-api-application-resource-delete
        /// </summary>
        /// <param name="resource"></param>
        void UnregisterResource(string resourceId);

        /// <summary>
        /// registers the specified resource on the APS controller
        /// https://doc.apsstandard.org/2.1/spec/api/application/instance-operations/#spec-api-application-resource-post
        /// </summary>
        /// <param name="resource"></param>
        void RegisterResource(ResourceBase resource);
        /// <summary>
        /// registers the specified resource on the APS controller
        /// https://doc.apsstandard.org/2.1/spec/api/application/instance-operations/#spec-api-application-resource-post
        /// </summary>
        /// <param name="resource"></param>
        void RegisterResource(string resourceId);

        /// <summary>
        /// updates the resource with the specified parameters.
        /// https://doc.apsstandard.org/2.1/spec/api/application/instance-operations/#spec-api-application-resource-put
        /// </summary>
        /// <param name="resource"></param>
        void UpdateResource(ResourceBase resource);

        /// <summary>
        /// changing properties of the specified resource via the APS controller
        /// https://doc.apsstandard.org/2.1/spec/api/resources/put/#spec-api-resource-put
        /// </summary>
        /// <param name="resource"></param>
        void ConfigureResource(ResourceBase resource);

        /// <summary>
        /// provision resource via the APS controller
        /// https://doc.apsstandard.org/2.1/spec/api/resources/post/#spec-api-resource-post
        /// </summary>
        /// <param name="resource"></param>
        void ProvisionResource(ResourceBase resource);

        /// <summary>
        /// Removes link linkName from resource1 to resource2
        /// https://doc.apsstandard.org/2.1/spec/api/resources/links/#spec-api-resource-unlinking
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="relationName"></param>
        /// <param name="linkResource"></param>
        void UnlinkResource(ResourceBase resource, string relationName, ResourceBase linkResource);
        /// <summary>
        /// Removes link linkName from resource1 to resource2
        /// https://doc.apsstandard.org/2.1/spec/api/resources/links/#spec-api-resource-unlinking
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="relationName"></param>
        /// <param name="linkResource"></param>        
        void UnlinkResource(string resourceId, string relationName, ResourceBase linkResource);
        /// <summary>
        /// Removes link linkName from resource1 to resource2
        /// https://doc.apsstandard.org/2.1/spec/api/resources/links/#spec-api-resource-unlinking
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="relationName"></param>
        /// <param name="linkResource"></param>
        void UnlinkResource(ResourceBase resource, string relationName, string linkResource);
        /// <summary>
        /// Removes link linkName from resource1 to resource2
        /// https://doc.apsstandard.org/2.1/spec/api/resources/links/#spec-api-resource-unlinking
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="relationName"></param>
        /// <param name="linkResource"></param>
        void UnlinkResource(string resource, string relationName, string linkResource);

        /// <summary>
        /// adds resource2 to the link collection linkName in resource1. If $resource2->aps->id is not defined, a new resource is created
        /// https://doc.apsstandard.org/2.1/spec/api/resources/links/#spec-api-resource-linking
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="relationName"></param>
        /// <param name="linkResource"></param>
        ResourceBase LinkResource(ResourceBase resource, string relationName, ResourceBase linkResource);
        /// <summary>
        /// adds resource2 to the link collection linkName in resource1. If $resource2->aps->id is not defined, a new resource is created
        /// https://doc.apsstandard.org/2.1/spec/api/resources/links/#spec-api-resource-linking
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="relationName"></param>
        /// <param name="linkResource"></param>
        /// <returns></returns>
        ResourceBase LinkResource(string resource, string relationName, ResourceBase linkResource);
        /// <summary>
        /// adds resource2 to the link collection linkName in resource1. If $resource2->aps->id is not defined, a new resource is created
        /// https://doc.apsstandard.org/2.1/spec/api/resources/links/#spec-api-resource-linking
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="relationName"></param>
        /// <param name="linkResource"></param>
        /// <returns></returns>
        ResourceBase LinkResource(ResourceBase resource, string relationName, string linkResource);
        /// <summary>
        /// adds resource2 to the link collection linkName in resource1. If $resource2->aps->id is not defined, a new resource is created
        /// https://doc.apsstandard.org/2.1/spec/api/resources/links/#spec-api-resource-linking
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="relationName"></param>
        /// <param name="linkResource"></param>
        /// <returns></returns>
        ResourceBase LinkResource(string resource, string relationName, string linkResource);

        /// <summary>
        /// requests a resource from the APS controller by resource ID
        /// https://doc.apsstandard.org/2.1/spec/api/resources/get/#spec-api-resource-get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResourceBase GetResource(string id);

        T GetResource<T>(string id);

        /// <summary>
        /// returns an array of all resources registered on the APS controller
        /// <see cref="https://doc.apsstandard.org/2.1/spec/api/resources/get/#spec-api-resource-list"/> more info here
        /// </summary>
        /// <param name="rqlFilter"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        object GetResources(string rqlFilter, string path);
    }
}
