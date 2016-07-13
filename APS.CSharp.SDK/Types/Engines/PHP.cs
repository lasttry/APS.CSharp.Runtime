using APS.CSharp.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.SDK.Types.Engines
{
    /// <summary>
    /// An interface of the “PHP” type provides the PHP script language with a certain configuration. Such interface may define a PHP version, a set of extensions and settings.
    /// </summary>
    [ResourceBase(Id = "http://aps-standard.org/types/engines/php/1.0")]
    public class PHP
    {
        [Property(Required = true)]
        public string Version { get; set; }

        public List<string> Extensions { get; set; }

        public string Memory_limit { get; set; }

        public string Max_execution_time { get; set; }

        public string Post_max_size { get; set; }

        public string Upload_max_filesize { get; set; }

        public string Safe_mode { get; set; }

        public string Safe_mode_include_dir { get; set; }

        public string Safe_mode_exec_dir { get; set; }

        public string Include_path { get; set; }

        public string Session_save_path { get; set; }

        public string Mail_force_extra_parameters { get; set; }

        public string Register_globals { get; set; }

        public string Open_basedir { get; set; }

        public string Error_reporting { get; set; }

        public string Display_errors { get; set; }
        public string Log_errors { get; set; }

        public string Allow_url_fopen { get; set; }

        public string File_uploads { get; set; }

        public string Short_open_tag { get; set; }

        public string Magic_quotes_qpc { get; set; }
    }
}
