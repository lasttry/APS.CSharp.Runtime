using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.VisualStudio
{
    [ComVisible(true)]
    [Guid("F883D5E5-30FA-4BBE-9901-0F56B2BABADE")]
    public class APSSchemaGenerator : BaseCodeGeneratorWithSite
    {
        public override string GetDefaultExtension()
        {
            // TODO: Replace with your implementation
            return ".cs";
        }

        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            byte[] result = new byte[0];
            try {
                // TODO: Replace with your implementation
                var t = new
                {
                    test = 1,
                    nome = "nome"
                };
                result = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(t));
            }catch(Exception e)
            {
                result = Encoding.ASCII.GetBytes(e.Message);
            }
            return result;
        }
    }
}
