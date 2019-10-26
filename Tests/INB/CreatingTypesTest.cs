using System;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using INB;
using NUnit.Framework;

namespace Tests
{
    public class CreatingTypesTest
    {
        [TestCase(1d, 5, 0.0001d, ExpectedResult = 1d)]
        [TestCase(8d, 3, 0.0001d, ExpectedResult = 2d)]
        [TestCase(0.001d, 3, 0.0001d, ExpectedResult = 0.1d)]
        [TestCase(0.04100625d, 4, 0.0001d, ExpectedResult = 0.45d)]
        [TestCase(8d, 3, 0.0001d, ExpectedResult = 2d)]
        [TestCase(0.0279936d, 7, 0.0001d, ExpectedResult = 0.6d)]
        [TestCase(-0.008d, 3, 0.1d, ExpectedResult = -0.2d)]
        public double Test_FindNthRoot(double number, int degree, double precision)
        {
            var result = CreatingTypes.FindNthRoot(number, degree, precision);

            return result;
        }
        
        [Test]
        public void Test_BubbleSortSum()
        {
            var matrix = new int[3][];
            matrix[0] = new int[] {12, 34, 1, 43, 56};
            matrix[1] = new int[] {96, 54, 23, 30, 49};
            matrix[2] = new int[] {24, 21, 90, 87, 41};
            CreatingTypes.BubbleSortSum(ref matrix, true);
            
            Assert.AreEqual(new[] {
                new int[] {24, 21, 90, 87, 41},
                new int[] {96, 54, 23, 30, 49},
                new int[] {12, 34, 1, 43, 56} 
            }, matrix);
        }
        
        [Test]
        public void Test_BubbleSortMax()
        {
            var matrix = new int[3][];
            matrix[0] = new int[] {12, 34, 1, 43, 56};
            matrix[1] = new int[] {96, 54, 23, 30, 49};
            matrix[2] = new int[] {24, 21, 90, 87, 41};
            CreatingTypes.BubbleSortMax(ref matrix, true);
            
            Assert.AreEqual(new[] {
                new int[] {96, 54, 23, 30, 49},
                new int[] {24, 21, 90, 87, 41},
                new int[] {12, 34, 1, 43, 56} 
            }, matrix);
        }
        
        [Test]
        public void Test_BubbleSortMin()
        {
            var matrix = new int[3][];
            matrix[0] = new int[] {12, 34, 1, 43, 56};
            matrix[1] = new int[] {96, 54, 23, 30, 49};
            matrix[2] = new int[] {24, 21, 90, 87, 41};
            CreatingTypes.BubbleSortMin(ref matrix, true);
            
            Assert.AreEqual(new[] {
                new int[] {12, 34, 1, 43, 56},
                new int[] {24, 21, 90, 87, 41},
                new int[] {96, 54, 23, 30, 49}
            }, matrix);
        }
    }
}