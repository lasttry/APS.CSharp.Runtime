using APS.CSharp.SDK.Attributes;

namespace APS.CSharp.Test.Types.Infrastructure
{
    [Structure]
    public struct Php
    {
        /// <summary>
        /// The resource property version defines the full version of a php interpreter.
        /// </summary>
        [Property(Required = false)]
        public string Version { get; set; }

        /// <summary>
        /// The resource array property extension defines all available php extensions.
        /// </summary>
        public string[] extensions { get; set; }

        /// <summary>
        /// Corresponding PHP Option: 
        /// allow_url_fopen
        /// </summary>
        public bool AllowUrlFopen { get; set; }

        /// <summary>
        /// Corresponding PHP Option: 
        /// file_uploads
        /// </summary>
        public bool FileUploads { get; set; }

        /// <summary>
        /// Corresponding PHP Option: 
        /// safe_mode
        /// </summary>
        public bool SafeMode { get; set; }

        /// <summary>
        /// Corresponding PHP Option: 
        /// short_open_tag
        /// </summary>
        public bool ShortOpenTags { get; set; }

        /// <summary>
        /// Corresponding PHP Option: 
        /// register_globals
        /// </summary>
        public bool RegisterGlobals { get; set; }

        /// <summary>
        /// Corresponding PHP Option: 
        /// magic_quotes_gpc
        /// </summary>
        public bool MagicQuotesGpc { get; set; }

        /// <summary>
        /// Corresponding PHP Option: 
        /// memory_limit in bytes.
        /// </summary>
        public int MemoryLimit { get; set; }
        /// <summary>
        /// Corresponding PHP Option: 
        /// max_execution_time in seconds.
        /// </summary>
        public int MaxExecutionTime { get; set; }
        /// <summary>
        /// Corresponding PHP Option: 
        /// post_max_size in bytes.
        /// </summary>
        public int PostMaxSize { get; set; }
    }
}
