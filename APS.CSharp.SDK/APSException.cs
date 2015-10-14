using System;

namespace APS.CSharp.SDK
{

    public class APSAsync : APSException
    {
        public APSAsync(int code, string message, int _timeout) : base(code, message)
        {
            timeout = _timeout;
        }
        public int timeout { get; set; }
    }
    
    public class APSException : Exception
    {
        public int Code { get; set; }
        public string Error { get; set; }
        public string Details { get; set; }
        public string AdditionalInfo { get; set; }

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
