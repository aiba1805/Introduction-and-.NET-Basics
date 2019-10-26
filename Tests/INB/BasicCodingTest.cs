using System;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using INB;
using NUnit.Framework;

namespace Tests
{
    public class BasicCodingTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(255, 200, 6, 3, ExpectedResult = 248)]
        public int Test_InsertNumber(int sourceNum, int inNum, int i, int j)
        {
            var result = BasicCoding.InsertNumber(sourceNum, inNum, j, i);

            return result;
        }

        [TestCase(new[] {22.3f, 44, 41, 35.2f, 31.1f}, ExpectedResult = 2)]
        public int Test_FindByEqualSum(float[] arr)
        {
            var res = BasicCoding.FindByEqualSum(arr);

            return res;
        }

        [TestCase("Hello", "Worldd", ExpectedResult = "HeloWorld")]
        public string Test_Concat(string first, string second)
        {
            var res = BasicCoding.Concat(first, second);

            return res;
        }

        [TestCase(12, 21)]
        [TestCase(513, 531)]
        [TestCase(2017, 2071)]
        [TestCase(414, 441)]
        [TestCase(144, 414)]
        [TestCase(1234321, 1241233)]
        [TestCase(1234126, 1234162)]
        [TestCase(3456432, 3462345)]
        [TestCase(10, 1)]
        [TestCase(20, 1)]
        public void Test_FindNextBiggerNumber(int number, int expected)
        {
            var result = BasicCoding.FindNextBiggerNumber(number);
            Console.Write($"Time elapsed: {result.Item2.Elapsed}");
            Assert.AreEqual(result.Item1, expected);
        }

        [TestCase(3, new[] {12, 10, 32, 33, 43, 22, 35, 11, 28}, ExpectedResult = new[] {32, 33, 43, 35})]
        [TestCase(1, new[] {12, 10, 32, 33, 43, 22, 35, 11, 28}, ExpectedResult = new[] {12, 10, 11})]
        [TestCase(2, new[] {12, 10, 32, 33, 43, 22, 35, 11, 28}, ExpectedResult = new[] {12, 32, 22, 28})]
        public int[] Test_FilterDigit(int number, int[] arr)
        {
            var result = BasicCoding.FilterDigit(number, arr);

            return result;
        }
    }
}