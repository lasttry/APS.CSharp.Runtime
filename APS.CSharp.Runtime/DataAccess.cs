using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace APS.CSharp.Runtime
{
    /// <summary>
    /// Does all the functions to save and retrive data
    /// Using SQLite database
    /// </summary>
    public class DataAccess
    {
        internal static string SQLiteName = "APS.CSharp.sqlite";
        internal static string SQLiteLogTable = "APS.CSharp.Runtime.SQLiteScripts.v1_0.logs.sql";
        internal static string SQLiteDatabase = "APS.CSharp.Runtime.SQLiteScripts.v1_0.database.sql";

        /// <summary>
        /// Opens the connection to the SQLite file where we will keep all the settings for the server
        /// </summary>
        /// <returns></returns>
        public static SQLiteConnection Connection()
        {
            string databaseFile = Path.Combine(HttpRuntime.AppDomainAppPath, SQLiteName);
            SQLiteConnection connection = new SQLiteConnection(databaseFile);

            connection.CreateTable<Database.Request>();
            connection.CreateTable<Database.APSRequests>();
            connection.CreateTable<Database.Bindings>();

            return connection;
        }

        public static void RequestsAdd(HttpRequest request, string contents)
        {
            var dbRequest = new Database.Request
            {
                Date = DateTime.Now,
                Url = request.RawUrl,
                ContentLenght = request.ContentLength,
                Headers = request.Headers.ToString(),
                HttpMethod = request.HttpMethod,
                Params = request.Params.ToString(),
                ServerVariables = request.ServerVariables.ToString(),
                Content = contents,
                UserHostname = request.UserHostName,
                UserHostAddress = request.UserHostAddress,
            };
            using (SQLiteConnection conn = Connection())
            {
                conn.Insert(dbRequest);
            }
        }

        public static List<Database.Request> RequestsGet()
        {
            using (SQLiteConnection conn = Connection())
            {
                List<Database.Request> results = new List<Database.Request>();
                foreach (Database.Request apsRequest in conn.Table<Database.Request>())
                    results.Add(new Database.Request(apsRequest));
                return results;
            }
        }

        public static Database.Request RequestsGet(int id)
        {
            using (SQLiteConnection conn = Connection())
            {
                return conn.Table<Database.Request>().Where(a => a.Id == id).FirstOrDefault();
            }
        }

        public static void APSRequestsAdd(DateTime date, string controllerUri, string identity, string instanceId, string requestPhase, string transactionId, string httpMethod, string url, string content)
        {
            var apsRequest = new Database.APSRequests
            {
                Date = date,
                ControllerUri = controllerUri,
                Identity = identity,
                InstanceId = instanceId,
                RequestPhase = requestPhase,
                TransationId = transactionId,
                HttpMethod = httpMethod,
                Url = url,
                Content = content
            };
            using(SQLiteConnection conn = Connection())
            {
                conn.Insert(apsRequest);
            }
        }

        public static List<Database.APSRequests> APSRequestsGet()
        {
            using (SQLiteConnection conn = Connection())
            {
                List<Database.APSRequests> results = new List<Database.APSRequests>();
                foreach (Database.APSRequests apsRequest in conn.Table<Database.APSRequests>())
                    results.Add(new Database.APSRequests(apsRequest));
                return results;
            }
        }

        public static Database.APSRequests APSRequestsGet(int id)
        {
            using (SQLiteConnection conn = Connection())
            {
                return conn.Table<Database.APSRequests>().Where(a => a.Id == id).FirstOrDefault();
            }
        }

        public static void BindingAdd(string instanceId, string apsType, string controllerUri, string x509Self, string x509Controller)
        {
            Database.Bindings binding = BindingGet(instanceId);
            if (binding != null)
                return;
            binding = new Database.Bindings
            {
                InstanceId = instanceId,
                APSType = apsType,
                ControllerUri = controllerUri,
                CertificateSelf = x509Self,
                CertificateController = x509Controller
            };
            using (SQLiteConnection conn = Connection())
            {
                conn.Insert(binding);
            }
        }

        public static List<Database.Bindings> BindingGet()
        {
            using (SQLiteConnection conn = Connection())
            {
                List<Database.Bindings> results = new List<Database.Bindings>();
                foreach (Database.Bindings apsRequest in conn.Table<Database.Bindings>())
                    results.Add(new Database.Bindings(apsRequest));
                return results;
            }
        }

        public static Database.Bindings BindingGet(string instanceId)
        {
            using(SQLiteConnection conn = Connection())
            {
                var controller = conn.Table<Database.Bindings>().Where(i => i.InstanceId == instanceId);
                if (controller == null)
                    return null;
                return controller.First();
            }
        }

        public static void ClearAllLogs()
        {
            string query1 = "DELETE FROM [request]";
            string query2 = "DELETE FROM [APSRequests]";
            using(SQLiteConnection conn = Connection())
            {
                conn.Execute(query1);
                conn.Execute(query2);
            }
        }
    }
}
