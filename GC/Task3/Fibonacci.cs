using System.Collections;
using System.Collections.Generic;

namespace GC.Task3
{
    public class Fibonacci
    {
        public static IEnumerable<int> FibonacciNumber(int number)
        {
            int prev = -1;
            int next = 1;
            for (int i = 0; i <= number; i++)
            {
                int sum = prev + next;
                prev = next;
                next = sum;
                yield return sum;
            }
        }
    }
}