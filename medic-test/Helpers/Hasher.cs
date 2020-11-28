using medic_api.Helpers;
using Xunit;

namespace medic_test
{
    public class Hasher
    {
        [Theory]
        [InlineData("", "")]
        public void HasherTest(string expected, string input)
        {
            Assert.Equal(expected, PasswordHasher.Hash(input));
        }

        [Theory]
        [InlineData("some test", "some test", true)]
        [InlineData("some test", "some test 2", false)]
        [InlineData("some test 2", "some test", false)]
        [InlineData("", "", false)]
        [InlineData("a", "", false)]
        [InlineData("", "a", false)]
        public void Verify(string str, string str2, bool equal)
        {
            var hash = PasswordHasher.Hash(str);
            Assert.Equal(equal, PasswordHasher.Verify(str2, hash));
        }
    }
}