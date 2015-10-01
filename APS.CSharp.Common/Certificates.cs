using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace APS.CSharp.Common
{
    public class Certificates
    {
        /// <summary>
        /// Using the bytes from the certificate it converts the Certificate and PrivateKey to one X509Certificate2 Object
        /// </summary>
        /// <param name="osaCertificate"></param>
        /// <returns>Complete X509Certificate2</returns>
        public static  X509Certificate2 GetFullCertificate(byte[] certificate)
        {
            // Gets the BEGIN CERTIFICATE / END CERTIFICATE part of the poa.pem
            byte[] certificateBytes = Crypto.GetBytesFromPEM(certificate, Crypto.PemStringType.Certificate);
            // Gets the BEGIN RSA PRIVATE KEY / END RSA PRIVATE KEY part of the poa.pem
            byte[] privateKeyBytes = Crypto.GetBytesFromPEM(certificate, Crypto.PemStringType.RsaPrivateKey);

            // initializes the object with the certificate bytes
            X509Certificate2 x509Certificate = new X509Certificate2(certificateBytes);

            // creating the private key and assigning it to the X509Certificate, if not we will always receive 403.
            RSACryptoServiceProvider prov = Crypto.DecodeRsaPrivateKey(privateKeyBytes);
            x509Certificate.PrivateKey = prov;

            return x509Certificate;
        }
    }
}
