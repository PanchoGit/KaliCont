using Kali.Security.Helpers;
using Xunit;

namespace Kali.UnitTest.Security.Helpers
{
    public class CryptographyHelperTest
    {
        [Fact]
        public void ShouldPassDecrypt()
        {
            var actual = CryptographyHelper.Decrypt("VLOf6pdQMRYGeqYv1889sA==");

            Assert.Equal("admin", actual);
        }

        [Fact]
        public void ShouldPassEncrypt()
        {
            var actual = CryptographyHelper.Encrypt("admin");

            Assert.Equal("VLOf6pdQMRYGeqYv1889sA==", actual);
        }
    }
}
