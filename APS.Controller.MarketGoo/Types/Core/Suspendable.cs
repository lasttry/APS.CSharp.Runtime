using APS.CSharp.SDK;
using APS.CSharp.SDK.Attributes;
using System.Net.Http;

namespace APS.CSharp.Test.Types.Core
{

    /// <summary>
    /// If an application is able to suspend and resume a service provided for a subscriber, this ability can be integrated with the APS controller in the following way:
    /// The APS type of the corresponding resource implements the Suspendable APS type.This exposes the enable and disable methods of the APS application endpoint to the APS controller.
    /// The APS application endpoint must handle the requests for the enable and disable methods correspondingly.
    /// This functionality is useful, when a customer suspends a services, or a subscription is about to expire.
    /// </summary>
    [ResourceBase(ApsVersion = "2.0", Id = "http://aps-standard.org/types/core/suspendable/1.0")]
    public class Suspendable
    {
        /// <summary>
        /// The enable operation returns a suspended resource to its original state.
        /// The status of the APS resource is changing to ready.
        /// </summary>
        /// <returns></returns>
        [Operation(Verb = HttpVerbs.PUT, Path = "/enable")]
        [Access(Owner = false, Referrer = false, Admin = true)]
        public string Enable()
        {
            return null;
        }

        /// <summary>
        /// The APS controller calls the disable method on request for service suspension. The resource should terminate the service available to the user (e.g. the user’s mailbox should no more be available through POP3/IMAP), but all data related to the resource should be kept.
        /// APS controller changes the status of the APS resource to disabled.
        ///  </summary>
        /// <returns></returns>
        [Operation(Verb = HttpVerbs.PUT, Path = "/disable")]
        [Access(Owner = false, Referrer = false, Admin = true)]
        public string Disable()
        {
            return null;
        }
    }
}
