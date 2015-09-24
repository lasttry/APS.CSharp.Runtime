using SQLite;
using System;

namespace APS.CSharp.Runtime.Database
{
    public class APSRequests
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ControllerUri { get; set; }
        public string Identity { get; set; }
        public string InstanceId { get; set; }
        public string RequestPhase { get; set; }
        public string TransationId { get; set; }
        public string HttpMethod { get; set; }
        public string Url { get; set; }
        public string Content{ get; set; }
        public APSRequests() { }
        public APSRequests(APSRequests apsRequest)
        {
            Id = apsRequest.Id;
            Date = apsRequest.Date;
            ControllerUri = apsRequest.ControllerUri;
            Identity = apsRequest.Identity;
            InstanceId = apsRequest.InstanceId;
            RequestPhase = apsRequest.RequestPhase;
            TransationId = apsRequest.TransationId;
            HttpMethod = apsRequest.HttpMethod;
            Url = apsRequest.Url;
            Content = apsRequest.Content;
        }
    }
}
