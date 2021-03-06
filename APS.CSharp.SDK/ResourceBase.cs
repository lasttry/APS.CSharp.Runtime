﻿using APS.CSharp.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace APS.CSharp.SDK
{
    public class CertificateElement
    {
        public string Self { get; set; }
        public string Controller { get; set; }
    }
    public class Package
    {
        public string Id { get; set; }
        public string Href { get; set; }
    }

    public class APS
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public string Status { get; set; }
        public int Revision { get; set; }
        public DateTime Modified { get; set; }
        public Package Package { get; set; }
        public CertificateElement X509 { get; set; }
    }

    /// <summary>
    /// The base for all of our resources.
    /// </summary>
    public class ResourceBase
    {
        public ResourceBase()
        {
            APS = new APS();
            APS.Type = this.GetType().GetCustomAttribute<ResourceBaseAttribute>().Id;
            APS.Modified = DateTime.Now;

        }
        public APS APS { get; set; }

        public IAPSC APSC { get; set; }

        public void Call()
        {
            throw new NotImplementedException();
        }
        public void Prepare()
        {
            throw new NotImplementedException();
        }

        public void PrepareData()
        {
            throw new NotImplementedException();
        }

        public void FillResource(string id, bool newSession = false)
        {

        }

        public void CheckGUID(string id)
        {
            try { Guid.Parse(id); }
            catch (Exception ex)
            {
                //TODO log exception in log resource
                Trace.TraceError(ex.Message);
                throw new Exception("Resource ID '" + id + "' is not in GUID format");
            }
        }

        public void PostProcess(string method, string args, string response)
        {
            throw new NotImplementedException();
        }

        public void GetSchema()
        {
            throw new NotImplementedException();
        }

        public void GetDefault()
        {
            throw new NotImplementedException();
        }
        
        public virtual void Retrieve() { }
        public virtual void Provision() { }
        public virtual void ProvisionAsync() { }
        public virtual void Configure<T>(T _new) { }
        public virtual void Unprovision() { }

        public void ToXml()
        {
            throw new NotImplementedException();
        }

        public void ToArray()
        {
            throw new NotImplementedException();
        }

    }
}
