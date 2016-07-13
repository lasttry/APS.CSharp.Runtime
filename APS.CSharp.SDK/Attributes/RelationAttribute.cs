using System;


namespace APS.CSharp.SDK.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RelationAttribute : Attribute
    {
        private bool _collection = false;
        private bool _required = false;
        private string _requirement = null;
        private AllocateEnum _allocate = AllocateEnum.Any;

        public bool Collection
        {
            get { return _collection; }
            set { _collection = value; }
        }
        /// <summary>
        /// This attribute explicitly defines if this relation is required or optional. A required relation must be established during resource provisioning and cannot be removed when the resource exists. Required relations are also known as strong relations. Optional relations are also known as weak relations.
        /// </summary>
        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }
        /// <summary>
        /// A relation may define which conditions must be met by a resource on the other side of the link. These conditions are expressed by means of the Resource Query Language (RQL).
        /// </summary>
        public string Requirement
        {
            get { return _requirement; }
            set { _requirement = value; }
        }
        /// <summary>
        /// This attribute allows you to define an allocation policy the APS Controller will use to satisfy strong (i.e. “required”: true) relations. 
        /// You can assign one of the following values to it:
        /// new -  forces unconditional allocation of a new resource.
        /// existing - forces the use of an existing resource.If no matching resources are found, it generates an error.
        /// any - tries to use the “existing” policy and forces the “new” in case of failure.
        /// </summary>
        public AllocateEnum Allocate
        {
            get { return _allocate; }
            set { _allocate = value; }
        }


    }
}
