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
        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }
        public string Requirement
        {
            get { return _requirement; }
            set { _requirement = value; }
        }
        public AllocateEnum Allocate
        {
            get { return _allocate; }
            set { _allocate = value; }
        }


    }
}
