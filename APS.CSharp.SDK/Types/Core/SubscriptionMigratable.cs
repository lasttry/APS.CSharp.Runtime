using APS.CSharp.SDK.Attributes;
using System.Net.Http;

namespace APS.CSharp.SDK.Types.Core
{
    /// <summary>
    /// Applicability: Odin Service Automation version 6.0.1 and newer
    /// This type allows the provider and resellers to migrate subscriptions between accounts.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/core/subscription/migratable/1.0")]
    public class SubscriptionMigratable
    {
        public MigrationSet MigrationSet { get; set; }

        public MigrationPreCheckResult MigrationPreCheckResult { get; set; }

        /// <summary>
        /// When the provider initiates migration of a subscription from the current subscriber to another, the APS controller calls the migrationPreCheck method at the resource that implements the Migratable type in this subscription. This method returns a structure that allows the system to proceed with the migration or not.
        /// The migrationPreCheck method accepts the MigrationSet structure in the message body that specifies APS ID of the target account resource.It returns the MigrationPreCheckResult structure containing two properties:
        /// canMigrate is mandatory.It specifies if the migration is allowed (true) or not(false).
        /// If migration is not allowed, the message property is a string that the system will display on the screen.
        /// To enable subscription migration, the resource implementing a management context type(also can be mentioned as tenant) must also implement the migratable type.The management context type was introduced in the relation overview section.
        /// </summary>
        /// <param name="migrationSet"></param>
        /// <returns></returns>
        [Operation(Path = "/migrationPreCheck", Verb = HttpVerbs.POST)]
        [Param(Kind = ParamSource.Body)]
        public MigrationPreCheckResult MigrationPreCheck(MigrationSet migrationSet)
        {
            string result = APSCUtility.APSC.SendRequest(APSCPaths.BuildResourcePath(APSCUtility.APSC.InstanceId, "/migrationPreCheck"), HttpMethod.Post,
                APSCUtility.APSC.ConvertObject2Json(migrationSet));

            return APSCUtility.APSC.ConvertJson2Object<MigrationPreCheckResult>(result);
        }
    }
}
