using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace medic_api.Helpers
{
    public class RSA
    {
        private static RSACryptoServiceProvider _cryptoServiceProvider = new RSACryptoServiceProvider(2048);
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;

        public RSA()
        {
            _publicKey = _cryptoServiceProvider.ExportParameters(true);
            _privateKey = _cryptoServiceProvider.ExportParameters(false);
        }

        public string PublicKeyString()
        {
            var stringWriter = new StringWriter();
            var xmlSerializer = new XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, _publicKey);
            return stringWriter.ToString();
        }

        public string Encrypt(string plainText)
        {
            _cryptoServiceProvider = new RSACryptoServiceProvider();
            _cryptoServiceProvider.ImportParameters(_publicKey);
            var data = Encoding.Unicode.GetBytes(plainText);
            var cypher = _cryptoServiceProvider.Encrypt(data, false);
            return Convert.ToBase64String(cypher);
        }

        public string Decrypt(string cypher)
        {
            var dataBytes = Convert.FromBase64String(cypher);
            _cryptoServiceProvider.ImportParameters(_privateKey);
            var plainText = _cryptoServiceProvider.Decrypt(dataBytes, false);
            return Encoding.Unicode.GetString(plainText);
        }
    }
}