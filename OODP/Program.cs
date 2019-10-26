using System;
using OODP.Matrix;

namespace OODP
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = 2;
            MatrixBase m1 = new MatrixSymmetric(size), m2 = new MatrixSymmetric(size);
            Random _random = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    m1[i, j] = _random.Next(1, 100);
                    m2[i, j] = _random.Next(1, 100);
                }
            }
            Console.WriteLine("Первая матрица:\n"+m1);
            Console.WriteLine("Вторая матрица:\n"+m2);
            Console.WriteLine("Сложенная матрица:\n"+(m1+m2));
        }
    }
}