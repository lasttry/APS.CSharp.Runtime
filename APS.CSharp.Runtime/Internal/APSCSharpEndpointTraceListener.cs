
using System;
using System.Diagnostics;

namespace APS.CSharp.Runtime
{
    /// <summary>
    /// Class to perform all our traces and debug information
    /// This has to be configured in the web.config folder
    /// </summary>
    public class APSCSharpEndpointTraceListener : TextWriterTraceListener
    {
        public APSCSharpEndpointTraceListener(string fileName) : base(fileName)
        {
        }

        /// <summary>
        /// Writes the message with the format we defined
        /// </summary>
        /// <param name="message"></param>
        public override void Write(string message)
        {
            base.Write(String.Format("[{0}]:{1}", DateTime.Now, message));
        }

        /// <summary>
        /// Writes the message with the format we defined
        /// </summary>
        /// <param name="message"></param>
        public override void WriteLine(string message)
        {
            base.WriteLine(String.Format("[{0}]:{1}", DateTime.Now, message));
        }

        /// <summary>
        /// Override to clear the message of unwanted information.
        /// </summary>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            
            string traceMessage = "";

            // It help us having only one function where the format is performed, if args is null, them just use the format string and avoid exceptions
            if (args != null)
                traceMessage = string.Format(format, args);
            else
                traceMessage = format;
            traceMessage = string.Format("[{0}]:{1}", eventType.ToString(), traceMessage);

            WriteLine(traceMessage);
        }
        /// <summary>
        /// Override to call the TraceEvent where we handle all TraceEvent calls
        /// </summary>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id) 
        {
            TraceEvent(eventCache, source, eventType, id, "", null);
        }
        /// <summary>
        /// Override to call the TraceEvent where we handle all TraceEvent calls
        /// </summary>

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            TraceEvent(eventCache, source, eventType, id, message, null);
        }


    }
}
