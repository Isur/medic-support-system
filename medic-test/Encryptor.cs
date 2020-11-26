using System;
using medic_api.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace medic_test
{
    public class Encryptor
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Encryptor(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Encrypt()
        {
            RSA rsa = new RSA();
            _testOutputHelper.WriteLine(rsa.PublicKeyString());
            
            string testCase = "Some random test";
            _testOutputHelper.WriteLine(testCase);
            var encrypted= rsa.Encrypt(testCase);
            _testOutputHelper.WriteLine(encrypted);
            var decrypted = rsa.Decrypt(encrypted);
            _testOutputHelper.WriteLine(decrypted);
            Assert.Equal(testCase, decrypted);
        }
    }
}