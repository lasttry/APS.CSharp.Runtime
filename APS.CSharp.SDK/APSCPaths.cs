using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace APS.CSharp.SDK
{
    public class APSCPaths
    {
        /// <summary>
        /// Gets the base path of the major version
        /// Uses the class version to understand in what version we are working on.
        /// Usually /asp/2/
        /// </summary>
        public static string BaseAPSCPath
        {
            get
            {
                int majorVersion = Assembly.GetExecutingAssembly().GetName().Version.Major;
                return string.Format("/aps/{0}/", majorVersion);
            }
        }

        /// <summary>
        /// Returns the resource path to work with APSC
        /// usually /aps/2/resources/
        /// </summary>
        public static string ResourcePath { get { return string.Format("{0}{1}/", BaseAPSCPath, "resources"); } }

        /// <summary>
        /// Return the application path to work directly with the Application resources
        /// usually /aps/2/application/
        /// </summary>
        public static string ApplicationPath { get { return string.Format("{0}{1}/", BaseAPSCPath, "application"); } }

        /// <summary>
        /// Build a path for the resources using arguments for the suffix
        /// </summary>
        /// <param name="paths">the arguments that will build the path, in order.</param>
        /// <returns>the complete built path</returns>
        public static string BuildResourcePath(params string[] paths)
        {
            return BuildPaths(ResourcePath, paths);
        }

        /// <summary>
        /// Builds a path for the application using the arguments for the suffix
        /// </summary>
        /// <param name="paths">the arguments that will build the path, in order.</param>
        /// <returns>the complete application path.</returns>
        public static string BuildApplicationPath(params string[] paths)
        {
            return BuildPaths(ApplicationPath, paths);
        }

        /// <summary>
        /// Buils a path using the supplied arguments
        /// </summary>
        /// <param name="basePath">The startup path before adding the arguments</param>
        /// <param name="paths">The arguments to add to the path</param>
        /// <returns>The complete build path.</returns>
        internal static string BuildPaths(string basePath, params string[] paths)
        {
            string path = basePath;
            foreach (string p in paths)
                path = VirtualPathUtility.Combine(path, p);
            return path;
        }
    }
}
