using System.Reflection.Metadata.Ecma335;

namespace Calculator
{
    public class Calculator
    {
        public int Sum(int number1, int number2)
        {
            return number1 + number2;
        }

        public int Subtract(int number1, int number2)
        {
            return number1 - number2;
        }

        public int Multiply(int number1, int number2)
        {
            return number1 * number2;
        }

        public float Divide(int number1, int number2)
        {

            if (number2==0)
            {
                throw new DivideByZeroException();
            }
            return number1 / (float)number2;
        }
    }
}