using APS.CSharp.SDK;
using APS.CSharp.SDK.Attributes;
using System.Net.Http;

namespace APS.CSharp.Test.Types.Core
{
    /// <summary>
    /// If an APS resource provides backup/restore functionality, is should expose it with help of the backupable interface.
    /// </summary>
    [ResourceBase(ApsVersion = "2.0", Id = "http://aps-standard.org/types/core/backupable/1.0")]
    public class Backupable
    {
        /// <summary>
        /// This method is called by the APS Controller on creation of a binary copy (backup) of a resource. As an implementation of the method, the application should return a binary block that may be restored later.
        /// </summary>
        /// <returns></returns>
        [Operation(Verb = HttpVerbs.GET, Path = "/backup")]
        public byte[] Backup()
        {
            return null;
        }

        /// <summary>
        /// This operation restores a resource from a binary copy created earlier by the backup operation. The APS Controller calls this method of the resource when it wants to revert the resource to the state preserved during the backup operation.
        /// </summary>
        /// <param name="backupData"></param>
        /// <returns></returns>
        [Operation(Verb = HttpVerbs.PUT, Path = "/restore")]
        [Param(Kind = ParamSource.Body)]
        public string Restore(byte[] backupData)
        {
            return null;
        }
    }
}
