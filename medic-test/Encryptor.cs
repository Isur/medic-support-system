using System;
using medic_api.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace medic_test
{
    public class Encryptor
    {
        [Fact]
        public void Encrypt()
        {
            RSA rsa = new RSA();
            RSA rsa2 = new RSA();
            string testCase = "Admin123 jakis test /n\n";
            var encrypted= rsa.Encrypt(testCase);
            var decrypted = rsa2.Decrypt(encrypted);
            Assert.Equal(testCase, decrypted);
        }
    }
}