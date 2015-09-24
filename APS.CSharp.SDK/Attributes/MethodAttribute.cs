using System;

namespace APS.CSharp.SDK.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MethodAttribute : Attribute
    {
        private bool _static = false;
        public bool Static
        {
            get { return _static; }
            set { _static = value; }
        }
        public HttpVerbs Verb { get; set; }
        public String Path { get; set; }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ParamAttribute : Attribute
    {
        public string Name { get; set; }
        public ParamSource Source { get; set; }
        public Type Type { get; set; }
    }
}
