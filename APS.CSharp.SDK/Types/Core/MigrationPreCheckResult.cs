using APS.CSharp.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.SDK.Types.Core
{

    [Structure]
    public class MigrationPreCheckResult
    {
        [Property(Required = true, Description = "True if migration is allowed")]
        public bool CanMigrate { get; set; }

        [Property(Description = "This string will be shown to user in case of migration is not possible")]
        public string Message { get; set; }
    }
}
