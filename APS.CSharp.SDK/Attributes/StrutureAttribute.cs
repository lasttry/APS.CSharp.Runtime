using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.SDK.Attributes
{
    public class StructureAttribute : Attribute
    {
        private string _type = "object";
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public string Description { get; set; }
    }
}
