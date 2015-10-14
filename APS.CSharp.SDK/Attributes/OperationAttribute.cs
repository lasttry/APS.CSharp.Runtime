using System;

namespace APS.CSharp.SDK.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class OperationAttribute : Attribute
    {
        private string _response;
        private string _errorResponse;
        private bool _static = false;

        /// <summary>
        /// Method name is used in documentation and code generation.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This attribute defines a path (relative to the resource instance root), which corresponds to the operation. It is possible to define parameters in the URL. In this case, names of the parameters must be presented in the path and embraced in {}, like: “{paramX}”
        /// Limitations
        /// The path value must be a string starting with a letter and containing letters, numbers, and underscores, i.e., it must match the regular expression[a - zA - Z][0-9a-zA-Z_]*.
        /// There should not be two operations that declare the same path/method.
        /// An operation cannot have a path that matches a relation name, for example, there should not be a path “/ves” if we have a relation “ves”.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Defines an HTTP method required to invoke the operation. See [RFC-2616-sec9] for the detailed description of these standard methods.
        /// </summary>
        public HttpVerbs Verb { get; set; }

        /// <summary>
        /// response can be specified as a valid JSON schema, just like any entry of the Structures array. The following example shows how a declaration of the “string” type
        /// It is possible to declare a non-JSON format as a response. To do that, the contentType attribute that contains a declaration of a valid MIME Media Type [RFC-2046] must be used. 
        /// WARNING
        /// Simultaneous usage of the contentType and type attributes is not supported and will produce an error, because the type implicitly declares the application/json MIME type.
        /// </summary>
        public string Response { get { return _response; } set { _response = value; } }

        /// <summary>
        /// errorResponse works like the response element, but declares the return format that will be used in case of method failure.
        /// It is NOT possible to declare a contentType element in the errorResponse.
        /// </summary>
        public string ErrorResponse { get { return _errorResponse; } set { _errorResponse = value; } }

        public bool Static { get { return _static; } set { _static = value; } }

    }


}
