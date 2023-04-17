namespace Calculator.Test
{
    public class UnitTest1
    {
        private Calculator _instance;

        public UnitTest1()
        {
            _instance = new Calculator();
        }

        [Fact]
        public void Test1()
        {
            Assert.Equal(1, 1);
        }

        [Fact]
        public void GivenCalculator_WhenAddingFiveAndThree_ShouldReturnEight()
        {
            //Act
            var actualResult = _instance.Sum(5, 3);

            //Assert
            Assert.Equal(8, actualResult);
        }

        [Fact]
        public void GivenCalculator_WhenSubtractingFiveAndThree_ShouldReturnTwo()
        {
            //Act
            var actualResult = _instance.Subtract(5, 3);

            //Assert
            Assert.Equal(2, actualResult);
        }

        [Fact]
        public void GivenCalculator_WhenMultiplyFiveAndThree_ShouldReturn15()
        {
            //Act
            var actualResult = _instance.Multiply(5, 3);

            //Assert
            Assert.Equal(15, actualResult);
        }       
        
        [Fact]
        public void GivenCalculator_WhenDivide20by4_ShouldReturn5()
        {
            //Act
            var actualResult = _instance.Divide(20, 4);

            //Assert
            Assert.Equal(5, actualResult);
        }

        [Fact]
        public void GivenCalculator_WhenDivide20by8_ShouldReturn2_5()
        {
            //Act
            var actualResult = _instance.Divide(20, 8);

            //Assert
            Assert.Equal(2.5, actualResult);
        }

        [Fact]
        public void GivenCalculator_WhenTheSecondNumberIs0_ShouldReturnADivideByZeroException()
        {

            //Assert
            Assert.Throws<DivideByZeroException>(() => { _instance.Divide(20, 0); });
        }
    }
}