using APS.CSharp.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.Test.Types.Infrastructure
{
    /// <summary>
    ///  resource of the “WebEnvironment” type represents a folder with files. Files and folders of the website must be accessible over the HTTP protocol on a domain.
    /// </summary>
    [ResourceBase(ApsVersion = "2.0", Id = "http://aps-standard.org/types/infrastructure/environment/web/1.0")]
    public class WebEnvironment : Environment
    {
        /// <summary>
        /// Path on the local file system to the folder with website files and directories. Filled by the environment resource.
        /// </summary>
        [Property(Required = false)]
        public string Path { get; set; }

        /// <summary>
        /// List of access URLs for the website (actual in case of HTTP/HTTPS access or if the site have several aliases). As a minimum one item must be in the list. The first item of the list is considered to be the primary address. Filled by the environment resource.
        /// </summary>
        [Property(Required = true, MinItems = 1)]
        public List<string> Urls { get; set; }

        /// <summary>
        /// Folder in an APS package, which will be mapped to the root folder of the HTTP website which exposes the application. Defaults to htdocs unless something other specified by the application.
        /// </summary>
        [Property(Default = "htdocs")]
        public string Root { get; set; }

        /// <summary>
        /// Properties applied to directories
        /// </summary>
        public List<Directory> Directories { get; set; }
    }
}
