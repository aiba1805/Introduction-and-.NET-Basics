using System;
using BasicsOfOOP.Task3;
using NUnit.Framework;

namespace Tests.OOP
{
    public class PolynomialTest
    {
        [TestCase(new []{4,-1}, new[]{-3,5}, ExpectedResult = new[]{1,4})]
        public int[] PolynomialTest_Add(int[] a, int[] b)
        {
            var polyA = new Polynomial(a);
            var polyB = new Polynomial(b);

            var result = polyA + polyB;

            return result.Coefficents;
        }
        
        [TestCase(new []{5,-4,3}, new[]{3,-1,2}, ExpectedResult = new[]{2,-3,1})]
        public int[] PolynomialTest_Difference(int[] a, int[] b)
        {
            var polyA = new Polynomial(a);
            var polyB = new Polynomial(b);

            var result = polyA - polyB;

            return result.Coefficents;
        }
        
        [TestCase(new []{1,0,1,0,-3,-3,8,2,-5}, new[]{3,0,5,0,-4,-9,21}, ExpectedResult = new[]{3,8,-8,-18,26,-18,58,49,-93,-143,170,87,-105})]
        public int[] PolynomialTest_Multiply(int[] a, int[] b)
        {
            var polyA = new Polynomial(a);
            var polyB = new Polynomial(b);

            var result = polyA * polyB;

            return result.Coefficents;
        }
        
        [TestCase(new []{-42, 0, -12, 1}, new[]{-3, 1, 0, 0})]
        public void PolynomialTest_Division(int[] a, int[] b)
        {
            var polyA = new Polynomial(a);
            var polyB = new Polynomial(b);

            var result = polyA / polyB;

            Assert.AreEqual((result.Item1.Coefficents, result.Item2.Coefficents),(new[] {-27, -9, 1}, new[] {-123}));
        }
    }
}