using System;
using System.Linq;

namespace INB
{
    public class CreatingTypes
    {
        public static double FindNthRoot(double number, int degree, double precision)
        {
            var x0 = number/degree; // Шаг 1: Сделать начальное предположение x0
            // Шаг 2: 1/n((n-1)x0 + (degree/x0^number-1)))
            var x1 = (1 / (double)degree)  * (((degree - 1) * x0) +  (number/ Math.Pow(x0, degree - 1)));
            
            while (Math.Abs(x1 - x0) >= precision)// Шаг 3: пока не достигнута необходимя точность
            {
                x0 = x1; // переходим на следующую степень
                x1 = (1 / (double)degree)  * (((degree - 1) * x0) +  (number/ Math.Pow(x0, degree - 1))); // повторяем шаг 2
            }

            var pres = precision.ToString("R").Split('.')[1].Length;
            return Math.Round(x1,pres); 
        }

        public static void BubbleSortSum(ref int[][] matrix, bool increase)
        {
            int rows = matrix.GetUpperBound(0) + 1;
            for (var i = 0; i < rows; ++i)
            {
                for (var j = 0; j < rows-i-1; j++)
                {
                    if (matrix[j].Sum() > matrix[j+1].Sum() && !increase ||
                        matrix[j].Sum() < matrix[j+1].Sum() && increase )
                    {
                        var t = matrix[j];
                        matrix[j] = matrix[j + 1];
                        matrix[j + 1] = t;
                    }
                }
            }
        }
        
        public static void BubbleSortMax(ref int[][] matrix, bool decrease)
        {
            int rows = matrix.GetUpperBound(0) + 1;
            for (var i = 0; i < rows; ++i)
            {
                for (var j = 0; j < rows-i-1; j++)
                {
                    if (matrix[j].Max() > matrix[j+1].Max() && !decrease ||
                        matrix[j].Max() < matrix[j+1].Max() && decrease )
                    {
                        var t = matrix[j];
                        matrix[j] = matrix[j + 1];
                        matrix[j + 1] = t;
                    }
                }
            }
        }
        
        public static void BubbleSortMin(ref int[][] matrix, bool increase)
        {
            int rows = matrix.GetUpperBound(0) + 1;
            for (var i = 0; i < rows; ++i)
            {
                for (var j = 0; j < rows-i-1; j++)
                {
                    if (matrix[j].Min() < matrix[j+1].Min() && !increase ||
                        matrix[j].Min() > matrix[j+1].Min() && increase )
                    {
                        var t = matrix[j];
                        matrix[j] = matrix[j + 1];
                        matrix[j + 1] = t;
                    }
                }
            }
        }
    }
}