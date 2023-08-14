namespace Calculator.Tests
{
    public class CalcTests
    {
        [Fact]
        [Trait("", "UnitTest")]
        public void Sum_3_and_4_results_7()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(3, 4);

            //Assert
            Assert.Equal(7, result);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(2, 3, 5)]
        [InlineData(-2, -3, -5)]
        [InlineData(-2, 3, 1)]
        [Trait("", "UnitTest")]
        public void Sum_with_results(int a, int b, int exp)
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(a, b);

            //Assert
            Assert.Equal(exp, result);
        }

        [Fact]
        [Trait("", "UnitTest")]
        [Trait("TestForException", "yes")]

        public void Sum_MAX_and_1_throws_OverflowException()
        {
            var calc = new Calc();

            Assert.Throws<OverflowException>(() => calc.Sum(int.MaxValue, 1));
        }  

        [Fact]
        [Trait("", "UnitTest")]
        [Trait("TestForException", "yes")]
        public void Sum_MIN_and_N1_throws_OverflowException()
        {
            var calc = new Calc();

            Assert.Throws<OverflowException>(() => calc.Sum(int.MinValue, -1));
        }
    }
}