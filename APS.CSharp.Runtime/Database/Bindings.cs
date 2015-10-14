using SQLite;

namespace APS.CSharp.Runtime.Database
{
    public class Bindings
    {
        [PrimaryKey]
        public string InstanceId { get; set; }
        public string APSType { get; set; }
        public string ControllerUri { get; set; }
        public string CertificateSelf { get; set; }
        public string CertificateController { get; set; }

        public Bindings() { }
        public Bindings(Bindings binding)
        {
            InstanceId = binding.InstanceId;
            APSType = binding.APSType;
            ControllerUri = binding.ControllerUri;
            CertificateSelf = binding.CertificateSelf;
            CertificateController = binding.CertificateController;
        }
    }
}
