using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.SDK
{
    public class APSCUtility
    {
        private static IAPSC _aps = null;

        /// <summary>
        /// Access the APSC Library to work with the POA APSC
        /// </summary>
        public static IAPSC APSC
        {
            get
            {
                if(_aps == null)
                {
                    // Getting the bin folder
                    string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                    var asm = Assembly.Load("APS.CSharp.Runtime");
                    var type = asm.GetType("APS.CSharp.Runtime.APSC");
                    _aps = Activator.CreateInstance(type, System.Web.HttpContext.Current.Request) as IAPSC;
                    if (_aps == null)
                        throw new Exception("Can't find Runtime library for this operation. APSC.CSharp instalation is broken.");
                }
                return _aps;
            }
        }

        /// <summary>
        /// Converts a list of values into a KeyValuePair to create the formpostdata
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, string>> GetPostData(params string[] values)
        {
            if (values.Length % 2 != 0)
                throw new APSException(500, "Post Data contents received are invalid. Received {0} that is odd value.", values.Length.ToString());

            List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();

            for(int i = 0;i < values.Length; i++)
            {
                postData.Add(new KeyValuePair<string, string>(values[i++], values[i]));
            }

            return postData;
        }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }


    }
}
