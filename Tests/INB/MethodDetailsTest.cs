using System;
using INB;
using NUnit.Framework;

namespace Tests
{
    public class MethodDetailsTest
    {
        [TestCase(-255.255d, ExpectedResult = "1100000001101111111010000010100011110101110000101000111101011100")]
        [TestCase(255.255d, ExpectedResult = "0100000001101111111010000010100011110101110000101000111101011100")]
        [TestCase(4294967295.0d, ExpectedResult = "0100000111101111111111111111111111111111111000000000000000000000")]
        [TestCase(double.MinValue, ExpectedResult = "1111111111101111111111111111111111111111111111111111111111111111")]
        [TestCase(double.MaxValue, ExpectedResult = "0111111111101111111111111111111111111111111111111111111111111111")]
        [TestCase(double.Epsilon, ExpectedResult = "0000000000000000000000000000000000000000000000000000000000000001")]
        [TestCase(double.NaN, ExpectedResult = "1111111111111000000000000000000000000000000000000000000000000000")]
        [TestCase(double.NegativeInfinity, ExpectedResult = "1111111111110000000000000000000000000000000000000000000000000000")]
        [TestCase(double.PositiveInfinity, ExpectedResult = "0111111111110000000000000000000000000000000000000000000000000000")]
        [TestCase(-0.0d, ExpectedResult = "1000000000000000000000000000000000000000000000000000000000000000")]
        [TestCase(0.0d, ExpectedResult = "0000000000000000000000000000000000000000000000000000000000000000")]
        public string Test_ConvertDouble(double number)
        {
            var result = MethodDetails.ConvertDoubleToBinaryString(number);
            return result;
        }
        
        [TestCase(11,18,ExpectedResult = 1)]
        [TestCase(12,27,ExpectedResult = 3)]
        [TestCase(2,6,ExpectedResult = 2)]
        public int Test_EwclideGCD(int a,int b)
        {
            var res = GCD.EwclideGcdTime(a, b);
            Console.WriteLine($"Completed with: {res.Item2}");
            return res.Item1;
        }
        
        [TestCase(12,48,60,36,ExpectedResult = 12)]
        [TestCase(12,43,21,32,66,74,50,ExpectedResult = 1)]
        public int Test_EwclideGCD(params int[] arg)
        {
            var res = GCD.EwclideGcdTime(arg);
            Console.WriteLine($"Completed with: {res.Item2}");
            return res.Item1;
        }
        
        [TestCase(11,18,ExpectedResult = 1)]
        [TestCase(12,27,ExpectedResult = 3)]
        [TestCase(2,6,ExpectedResult = 2)]
        public int Test_SteineGcd(int a,int b)
        {
            var res = GCD.SteineGcdTime(a, b);
            Console.WriteLine($"Completed with: {res.Item2}");
            return res.Item1;
        }
        
        [TestCase(12,48,60,36,ExpectedResult = 12)]
        [TestCase(12,43,21,32,66,74,50,ExpectedResult = 1)]
        public int Test_SteineGcd(params int[] arg)
        {
            var res = GCD.SteineGcdTime(arg);
            Console.WriteLine($"Completed with: {res.Item2}");
            return res.Item1;
        }

        [TestCase(null,ExpectedResult = true)]
        [TestCase(1,ExpectedResult = false)]
        [TestCase("Kathy",ExpectedResult = false)]
        public bool Test_NullableExtensionString(object? val)
        {
            return val.IsNull();
        }
    }
}