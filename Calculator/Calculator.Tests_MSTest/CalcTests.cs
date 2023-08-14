namespace Calculator.Tests_MSTest
{
    [TestClass]
    public class CalcTests
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        public void Sum_3_and_4_results_7()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(3, 4);

            //Assert
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        [DataRow(0, 0, 0)]
        [DataRow(2, 3, 5)]
        [DataRow(-2, -3, -5)]
        [DataRow(-2, 3, 1)]
        [TestCategory("UnitTest")]
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