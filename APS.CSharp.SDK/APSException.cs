using System;

namespace APS.CSharp.SDK
{
    /// <summary>
    /// Specific Exception to handle the ASync methods of the endpoint.
    /// </summary>
    public class APSAsync : APSException
    {
        public APSAsync(int code, string message, int timeout) : base(code, message)
        {
            Timeout = timeout;
        }
        public int Timeout { get; set; }
    }
    
    /// <summary>
    /// APS Specific Exception
    /// All Libraries should use this class to expose the exception to the APSC
    /// </summary>
    public class APSException : Exception
    {
        public int Code { get; set; }
        public string Error { get; set; }
        public string Details { get; set; }
        public string AdditionalInfo { get; set; }

        public APSException() : base("An Unknown Exception Has Occurred") { }

        public APSException(string message, APSException innerException) : base(message, innerException) { }

        public APSException(string message): base(message)
        {
            Code = 500;
        }
        public APSException(int code, string message) : base(message)
        {
            Code = code;
        }

        public APSException(int code, string message, params string[] args) : base(string.Format(message, args))
        {
            Code = code;
        }
    }
}
