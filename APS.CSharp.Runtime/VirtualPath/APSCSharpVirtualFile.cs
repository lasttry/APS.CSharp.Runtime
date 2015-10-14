using System;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace APS.CSharp.Runtime.VirtualPath
{
    public class APSCSharpVirtualFile : VirtualFile
    {
        string path;
        public APSCSharpVirtualFile(string virtualPath) : base(virtualPath)
        {
            path = VirtualPathUtility.ToAppRelative(virtualPath);
        }
        public override System.IO.Stream Open()
        {
            string[] parts = path.Split('/');
            string assemblyName = parts[2];
            string resourceName = parts[3];

            assemblyName = Path.Combine(HttpRuntime.BinDirectory, assemblyName);

            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(assemblyName);
            if (assembly != null)
            {
                return assembly.GetManifestResourceStream(resourceName);
            }
            return null;
        }
    }
}
