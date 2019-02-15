using Xunit;

namespace Kali.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var count = 1;

            Assert.Equal(1, count);
        }
    }
}
