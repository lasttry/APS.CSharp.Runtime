using System;
using System.Collections.Generic;

namespace APS.CSharp.SDK.Attributes
{

    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyAttribute : Attribute
    {
        private string _description = null;
        private bool _required = false;
        private bool _readOnly = false;
        private bool _final = false;
        private bool _encrypted = false;
        private string _unit = null;
        private object _default = null;
        private string _format;
        string _pattern = null;
        string _title = null;
        bool _headline = false;
        int _minLength = -1;
        int _maxLength = -1;
        int _minItems = -1;
        int _maxItems = -1;
        bool _uniqueItems = false;
        List<string> _enum = null;
        List<string> _enumTitles = null;


        /// <summary>
        /// description is a string containing a full description of the resource property.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        /// <summary>
        /// required indicates whether the resource property must have a value and not be undefined (true value). 
        /// </summary>
        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }
        /// <summary>
        /// readonly attribute means that the resource property cannot be set or changed through the user interface 
        /// </summary>
        public bool ReadOnly { get { return _readOnly; } set { _readOnly = value; } }
        /// <summary>
        /// final indicates that the resource property can only be assigned a value on resource creation. In other words, the attribute allows a user or an application the POST and GET operations, but does not allow PUT.
        /// </summary>
        public bool Final { get { return _final; } set { _final = value; } }
        /// <summary>
        /// encrypted is used to mark sensitive pieces of data that must be encrypted before saving in database, e.g. passwords.
        /// </summary>
        public bool Encrypted { get { return _encrypted; } set { _encrypted = value; } }
        /// <summary>
        /// unit defines a measurement unit for the property.
        /// </summary>
        public string Unit { get { return _unit; } set { _unit = value; } }
        /// <summary>
        /// default indicates a default value (values) for the property to be used as a hint for those who uses the resource schema to prepare data for new resource creation.
        /// </summary>
        public object Default { get { return _default; } set { _default = value; } }
        /// <summary>
        /// format defines the type of data, content type, or microformat to be expected in the resource property values. It may be one of the values listed below, and if so, should adhere to the semantics describing the format. 
        /// </summary>
        public string Format { get { return _format; } set { _format = value; } }
        /// <summary>
        /// pattern is a regular expression that a property value must match in order to be valid. When pattern is present, the specificator from the format element is ignored. Regular expressions should follow the regular expression specification from ECMA 262/Perl 5 [ECMA-262].
        /// </summary>
        public string Pattern { get { return _pattern; } set { _pattern = value; } }
        /// <summary>
        /// title is a string that provides a short description of the instance property.
        /// </summary>
        public string Title { get { return _title; } set { _title = value; } }
        /// <summary>
        /// boolean property that identifies if this property is used to represent a resource (primarily used in auto-generated UI)
        /// </summary>
        public bool Headline { get { return _headline; } set { _headline = value; } }
        /// <summary>
        /// minLength defines the minimum length of the string value.
        /// </summary>
        public int MinLength { get { return _minLength; } set { _minLength = value; } }
        /// <summary>
        /// maxLength defines the maximum length of the string value.
        /// </summary>
        public int MaxLength { get { return _maxLength; } set { _maxLength = value; } }
        /// <summary>
        /// minItems defines the minimum number of values in an array when “array” is the property value.
        /// </summary>
        public int MinItems { get { return _minItems; } set { _minItems = value; } }
        /// <summary>
        /// maxItems defines the maximum number of values in an array when the array is the property value.
        /// </summary>
        public int MaxItems { get { return _maxItems; } set { _maxItems = value; } }
        /// <summary>
        /// uniqueItems indicates that all items in an array instance must be unique, i.e. the array does not contain two identical (equal) values.
        /// Two instances are considered equal(not unique) if they are both of the same type and:
        /// are null; or
        /// are booleans/numbers/strings and have the same value; or
        /// are arrays, contain the same number of items, and each item in one array is equal to the corresponding item in another array; or
        /// are objects, contain the same property names, and each property in one object is equal to the corresponding property in another object.
        /// </summary>
        public bool UniqueItems { get { return _uniqueItems; } set { _uniqueItems = value; } }
        /// <summary>
        /// enum provides an enumeration of all possible values that are valid for the resource property. If this attribute is defined, the property value must be one of the values in the array in order for the schema to be valid.
        /// Comparison of enum values uses the following algorithm.Two instances are considered equal if they are both of the same type and:
        /// are null; or
        /// are booleans/numbers/strings and have the same value; or
        /// are arrays, contain the same number of items, and each item in one array is equal to the corresponding item in another array; or
        /// are objects, contain the same property names, and each property in one object is equal to the corresponding property in another object.
        /// </summary>
        public List<string> Enum { get { return _enum; } set { _enum = value; } }
        /// <summary>
        /// enumTitles is an array of strings that provides short description of each possible value of the resource property.
        /// </summary>
        public List<string> EnumTitles { get { return _enumTitles; } set { _enumTitles = value; } }

         

    }
}
