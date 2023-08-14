namespace TddBank.Tests
{
    public class OpeningHoursTests
    {
        [Theory]
        [InlineData(2023, 8, 14, 10, 30, true)]//mo
        [InlineData(2023, 8, 14, 10, 29, false)]//mo
        [InlineData(2023, 8, 14, 10, 31, true)] //mo
        [InlineData(2023, 8, 14, 18, 59, true)] //mo
        [InlineData(2023, 8, 14, 19, 00, false)] //mo
        [InlineData(2023, 8, 19, 13, 0, true)] //sa
        [InlineData(2023, 8, 19, 16, 0, false)] //sa
        [InlineData(2023, 8, 20, 20, 0, false)] //so
        public void OpeningHours_IsOpen(int y, int M, int d, int h, int m, bool result)
        {
            var dt = new DateTime(y, M, d, h, m, 0);
            var oh = new OpeningHours();

            Assert.Equal(result, oh.IsOpen(dt));
        }

        [Fact]
        public void Wed()
        {
            var oh = new OpeningHours();
            oh.IsOpen(new DateTime(2023, 8, 16));
        }
    }
}
