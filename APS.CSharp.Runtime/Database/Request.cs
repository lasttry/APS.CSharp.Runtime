using SQLite;
using System;

namespace APS.CSharp.Runtime.Database
{
    /// <summary>
    /// The schema for the Request Table in the database
    /// </summary>
    public class Request
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public int ContentLenght { get; set; }
        public string Headers { get; set; }
        public string HttpMethod { get; set; }
        public string Params { get; set; }
        public string ServerVariables { get; set; }
        public string Content { get; set; }
        public string UserHostname { get; set; }
        public string UserHostAddress { get; set; }

        public Request() { }

        public Request(Request request)
        {
            Id = request.Id;
            Date = request.Date;
            Url = request.Url;
            ContentLenght = request.ContentLenght;
            Headers = request.Headers;
            HttpMethod = request.HttpMethod;
            Params = request.Params;
            ServerVariables = request.ServerVariables;
            Content = request.Content;
            UserHostname = request.UserHostname;
            UserHostAddress = request.UserHostAddress;
        }
    }
}
