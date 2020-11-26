using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace medic_api.Helpers
{
    public class DataEncryptor
    {
    private readonly string _publicKeyString;
    private readonly string _privateKeyString;
        public DataEncryptor()
        {
            var cryptoServiceProvider = new RSACryptoServiceProvider(2048);
            var privateKey = cryptoServiceProvider.ExportParameters(true);
            var publicKey = cryptoServiceProvider.ExportParameters(false);

            _publicKeyString = _getKeyString(publicKey);
            _privateKeyString = _getKeyString(privateKey);
        }
        
        public string Encrypt(string data)
        {
            var bytesToEncode = Encoding.UTF8.GetBytes(data);
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    rsa.FromXmlString(_publicKeyString);
                    var encryptedData = rsa.Encrypt(bytesToEncode, true);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        public string Decrypt(string data)
        {
            var bytesToDecrypt = Encoding.UTF8.GetBytes(data);
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    rsa.FromXmlString(_privateKeyString);
                    var resultBytes = Convert.FromBase64String(data);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        private string _getKeyString(RSAParameters publicKey)
        {
            var stringWriter = new StringWriter();
            var xmlSerializer = new XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, publicKey);
            return stringWriter.ToString();
        }
    }
}