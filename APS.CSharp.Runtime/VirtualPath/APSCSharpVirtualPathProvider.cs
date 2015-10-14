using System;
using System.Web;
using System.Web.Hosting;

namespace APS.CSharp.Runtime.VirtualPath
{
    public class APSCSharpVirtualPathProvider : VirtualPathProvider
    {
        public APSCSharpVirtualPathProvider() { }
        private bool IsAppResourcePath(string virtualPath)
        {
            String checkPath =
               VirtualPathUtility.ToAppRelative(virtualPath);
            return checkPath.StartsWith("~/Site/", StringComparison.InvariantCultureIgnoreCase);
        }
        public override bool FileExists(string virtualPath)
        {
            return (IsAppResourcePath(virtualPath) || base.FileExists(virtualPath));
        }
        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsAppResourcePath(virtualPath))
                return new APSCSharpVirtualFile(virtualPath);
            else
                return base.GetFile(virtualPath);
        }
        public override System.Web.Caching.CacheDependency GetCacheDependency(string virtualPath, System.Collections.IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (IsAppResourcePath(virtualPath))
                return null;
            else
                return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }
    }
}
