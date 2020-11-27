using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace medic_api.Helpers
{
    public class RSA
    {
        private static RSACryptoServiceProvider _cryptoServiceProvider = new RSACryptoServiceProvider(2048);
        private readonly RSAParameters _privateKey;
        private readonly RSAParameters _publicKey;

        public RSA()
        {
            _privateKey = _fileToKey("privateKey.xml");
            _publicKey = _fileToKey("publicKey.xml");
        }

        private RSAParameters _fileToKey(string filename)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filename);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(RSAParameters));
                XmlReader xmlReader = new XmlNodeReader(xmlDocument.DocumentElement);
                var key = (RSAParameters) xmlSerializer.Deserialize(xmlReader);
                return key;
            }
            catch
            {
                var key =_cryptoServiceProvider.ExportParameters(true);
                _keyToFile(_keyToString(key), filename);
                return key;
            }
        }

        private void _keyToFile(string key, string filename)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(key);
            xmlDocument.Save(filename);
        }

        private string _keyToString(RSAParameters key)
        {
            var stringWriter = new StringWriter();
            var xmlSerializer = new XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, key);
            return stringWriter.ToString();
        }

        public string Encrypt(string plainText)
        {
            _cryptoServiceProvider = new RSACryptoServiceProvider();
            _cryptoServiceProvider.ImportParameters(_publicKey);
            var data = Encoding.UTF8.GetBytes(plainText);
            var cypher = _cryptoServiceProvider.Encrypt(data, true);
            return Convert.ToBase64String(cypher);
        }

        public string Decrypt(string cypher)
        {
            var dataBytes = Convert.FromBase64String(cypher);
            _cryptoServiceProvider.ImportParameters(_privateKey);
            var plainText = _cryptoServiceProvider.Decrypt(dataBytes, true);
            return Encoding.UTF8.GetString(plainText);
        }
    }
}