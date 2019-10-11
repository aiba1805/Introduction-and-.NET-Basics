using System;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using BasicCoding;
using NUnit.Framework;

namespace Tests
{
    public class BasicCodingTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_InsertNumber()
        {
            //arrange
            var sourceNum = 255;
            var inNum = 200;
            var i = 6;
            var j = 3;
            //act
            var result = Tasks.InsertNumber(sourceNum, inNum, j, i);

            Assert.AreEqual(result, 248);
        }

        [Test]
        public void Test_FindByEqualSum()
        {
            float[] arr = {22.3f, 44, 41, 35.2f, 31.1f};

            var res = Tasks.FindByEqualSum(arr);
            
            Assert.AreEqual(res,2);
        }
        
        [Test]
        public void Test_Concat()
        {
            string first = "Hello", second = "Worldd";

            var res = Tasks.Concat(first,second);
            
            Assert.AreEqual(res,"HeloWorld");
        }

        [TestCase(12,21)]
        [TestCase(513,531)]
        [TestCase(2017,2071)]
        [TestCase(414,441)]
        [TestCase(144,414)]
        [TestCase(1234321,1241233)]
        [TestCase(1234126,1234162)]
        [TestCase(3456432,3462345)]
        [TestCase(10, 1)]
        [TestCase(20, 1)]
        public void Test_FindNextBiggerNumber(int number, int expected)
        {
            var result = Tasks.FindNextBiggerNumber(number);
            Console.Write($"Time elapsed: {result.Item2.Elapsed}");
            Assert.AreEqual(result.Item1,expected);
        }
        
        [TestCase(3, new []{12, 10, 32, 33, 43 ,22 ,35, 11, 28}, ExpectedResult = new []{32,33,43,35})]
        [TestCase(1, new []{12, 10, 32, 33, 43 ,22 ,35, 11, 28}, ExpectedResult = new []{12,10,11})]
        [TestCase(2, new []{12, 10, 32, 33, 43 ,22 ,35, 11, 28}, ExpectedResult = new []{12,32,22,28})]
        public int[] Test_FilterDigit(int number, int[] arr)
        {
            var result = Tasks.FilterDigit(number, arr);

            return result;
        }


    }
}