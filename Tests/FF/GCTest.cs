using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FF.Task8;
using GC.Task1;
using GC.Task2;
using GC.Task3;
using GC.Task6;
using NUnit.Framework;

namespace Tests.FF
{
    public class GCTest
    {
        [TestCase(new []{1,3,5,7,9,54,77},3, ExpectedResult = 1)]
        public int BinarySearch_Test(int[] arr,int x)
        {
            var res = SearchAlgorithm.BinarySearch(arr, 0, arr.Length - 1, x, new IntComparer());
            return res;
        }

        [TestCase(12, ExpectedResult = 144)]
        public int FibonacciNumber_Test(int num)
        {
            var res = Fibonacci.FibonacciNumber(num);
            return res.Last();
        }

        [TestCase("text.txt", "Hello", ExpectedResult = 3)]
        public int WordFrequency_Test(string filename, string word)
        {
            var res = TextFileHelper.WordFrequency(filename, word);
            return res;
        }

        [TestCase(new[] {25, 32, 22, 46, 75}, ExpectedResult = "32 22 46 75")]
        public string Queue_Test(int[] arr)
        {
            var queue =  new GC.Task4.Queue<int>(arr.Length);
            foreach (var i in arr)
            {
                queue.Enqueue(i);
            }
            queue.Dequeue();
            return queue.ToString();
        }
        
        [TestCase(new[] {25, 32, 22, 46, 75}, ExpectedResult = 75)]
        public int Stack_Test(int[] arr)
        {
            var stack =  new GC.Task5.Stack<int>(arr.Length);
            foreach (var i in arr)
            {
                stack.Push(i);
            }
            return stack.Peek();;
        }
        
        [TestCase(new[] {22, 32, 22, 46, 75}, ExpectedResult = new[] {22,32,75})]
        public int[] Set_Test(int[] arr)
        {
            var set = new Set<int>();
            foreach (var i in arr)
            {
                set.Add(i);
            }
            set.Remove(46);
            return set.Items;
        }
        
        [TestCase("5 1 2 + 4 * + 3 -", ExpectedResult = 14)]
        public int RPN_Test(string expr)
        {
            var res = RPN.Evaluate(expr);
            return res;
        }
    }
}