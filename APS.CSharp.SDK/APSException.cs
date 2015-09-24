

namespace APS.CSharp.SDK
{
    public class APSException
    {
        public int code { get; set; }
        public string message { get; set; }

        public APSException(int _code, string _format, params string[] arg)
        {
            code = _code;
            message = string.Format(_format, arg);
        }

        public APSException(int _code, string _message)
        {
            code = _code;
            message = _message;
        }

        public APSException() { }
    }
}
