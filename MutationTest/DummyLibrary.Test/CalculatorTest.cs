namespace DummyLibrary.Test
{
    public class CalculatorTest
    {
        private Calculator _calc;

        public CalculatorTest()
        {
            _calc = new Calculator();
        }

        [Theory]
        [InlineData(1, 1, 2)]
        public void OnSum_WhenPassTwoIntegers_ShouldReturnTheExpectedValue(int number1, int number2, int expectedResult)
        {
            int actualResult = _calc.Sum(number1, number2);
            actualResult.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(1, 1, 0)]
        public void OnSubtract_WhenPassTwoIntegers_ShouldReturnTheExpectedValue(int number1, int number2, int expectedResult)
        {
            int actualResult = _calc.Subtract(number1, number2);
            actualResult.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        public void OnMultiply_WhenPassTwoIntegers_ShouldReturnTheExpectedValue(int number1, int number2, int expectedResult)
        {
            int actualResult = _calc.Multiply(number1, number2);
            actualResult.Should().Be(expectedResult);
        }


        [Theory]
        [InlineData(5, 1, 5)]
        public void OnDevide_WhenPassTwoIntegers_ShouldReturnTheExpectedValue(int number1, int number2, int expectedResult)
        {
            float actualResult = _calc.Devide(number1, number2);
            actualResult.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(5, 0)]
        public void OnDevide_WhenDevideByZero_ShouldThrow(int number1, int number2)
        {
            var func = () => _calc.Devide(number1, number2);
            func.Should().Throw<DivideByZeroException>();
        }
    }
}
