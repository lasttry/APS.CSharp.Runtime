using System;

namespace APS.CSharp.SDK.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ParamAttribute : Attribute
    {
        public string Name { get; set; }
        public ParamSource Source { get; set; }
        public Type Type { get; set; }
    }
}
