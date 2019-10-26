using BasicsOfOOP.Task1;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;

namespace Tests.OOP
{
    public class BubbleSortTest
    {
        [Test]
        public void Test_BubbleSortSum()
        {
            var matrix = new int[3][];
            matrix[0] = new int[] {12, 34, 1, 43, 56};
            matrix[1] = new int[] {96, 54, 23, 30, 49};
            matrix[2] = new int[] {24, 21, 90, 87, 41};
            BubbleSort.BubbleSortMatrix(ref matrix, ComparisonCriterion.Sum,Order.Ascen);
            
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
            BubbleSort.BubbleSortMatrix(ref matrix, ComparisonCriterion.Max,Order.Ascen);
            
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
            BubbleSort.BubbleSortMatrix(ref matrix, ComparisonCriterion.Min,Order.Ascen);
            
            Assert.AreEqual(new[] {
                new int[] {12, 34, 1, 43, 56},
                new int[] {24, 21, 90, 87, 41},
                new int[] {96, 54, 23, 30, 49}
            }, matrix);
        }
    }
}