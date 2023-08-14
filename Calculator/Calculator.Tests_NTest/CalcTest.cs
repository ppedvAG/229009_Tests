namespace Calculator.Tests_NTest
{
    public class Tests
    {
        [Test]
        [Category("UnitTest")]
        public void Sum_3_and_4_results_7()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(3, 4);

            //Assert
            Assert.AreEqual(7, result);
        }

        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(2, 3, 5)]
        [TestCase(-2, -3, -5)]
        [TestCase(-2, 3, 1)]
        [Category("UnitTest")]
        public void Sum_with_results(int a, int b, int exp)
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(a, b);

            //Assert
            Assert.AreEqual(exp, result);
        }
    }
}