using System;

namespace APS.CSharp.SDK.Attributes
{
    /// <summary>
    /// for some elements (JSON objects) of a resource schema, you can use access attributes to assign permissions for different security roles.
    /// When used in a property, the access attribute defines permissions to the property for APS roles.
    /// Value: Permission object
    /// Default: {“admin”: true, “owner”: true, “referrer”: false, “public”: false}
    /// access allows or disallows access to the resource property when a user is either reading or changing the property.When a user attempts to read a not allowed property, the APS controller does not display the property in the output. If a user attempts to submit a value to a not allowed property, an error is returned.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AccessAttribute : Attribute
    {
        private bool _admin = true;
        private bool _owner = true;
        private bool _referrer = false;
        private bool _public = false;

        public bool Admin
        {
            get { return _admin = true; }
            set { _admin = value; }
        }
        public bool Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public bool Referrer
        {
            get { return _referrer; }
            set { _referrer = value; }
        }
        public bool Public
        {
            get { return _public; }
            set { _public = value; }
        }
    }
}
