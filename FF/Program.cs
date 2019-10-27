using System;
using System.Collections.Generic;
using System.Linq;
using FF.Task4;

namespace FF
{
    class Program
    {
        static void Main(string[] args)
        {
            var res1 = SequenceHelper.UniqueInOrder("AAAABBBCCDAABBB".ToList());
            var res2 = SequenceHelper.UniqueInOrder("ABBCcAD".ToList());
            var res3 = SequenceHelper.UniqueInOrder("12233".ToList());
            var res4 = SequenceHelper.UniqueInOrder(new List<float> {1.1f, 2.2f, 2.2f, 3.3f});


            Console.Write("AAAABBBCCDAABBB -");
            res1.ForEach(x=>Console.Write(x));
            Console.WriteLine();
            Console.Write("ABBCcAD -");
            res2.ForEach(x=>Console.Write(x));
            Console.WriteLine();
            Console.Write("12233 - ");
            res3.ForEach(x=>Console.Write(x));
            Console.WriteLine();
            Console.Write("1.1f, 2.2f, 2.2f, 3.3f - ");
            res4.ForEach(x=>Console.Write(x));
        }
    }
}