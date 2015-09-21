using System.Collections.Generic;
using System.Reflection;

namespace APS.CSharp.Runtime.Internal
{
    public class Controller
    {
        public string FullName { get; set; }
        public Assembly Assembly { get; set; }
        public string Endpoint { get; set; }
        public List<string> Types { get; set; }
    }
}
