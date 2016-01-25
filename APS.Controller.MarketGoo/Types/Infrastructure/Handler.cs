﻿using APS.CSharp.SDK.Attributes;
using System.Collections.Generic;

namespace APS.CSharp.Test.Types.Infrastructure
{
    /// <summary>
    /// handlers element defines for the directory an array with handlers and their properties.
    /// </summary>
    [Structure]
    public struct Handler
    {
        public string Engine { get; set; }
        public bool Enabled { get; set; }
        public List<string> Extensions { get; set; }
    }
}
