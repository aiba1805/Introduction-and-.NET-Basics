using System;
using System.Linq;

namespace BasicsOfOOP.Task1
{
    public enum ComparisonCriterion
    {
        Sum,Max,Min
    }

    public enum Order
    {
        Ascen, Descen
    }

    public static class BubbleSort
    {
        public static void BubbleSortMatrix(ref int[][] matrix, ComparisonCriterion criterion, Order order)
        {
            int rows = matrix.GetUpperBound(0) + 1;
            for (var i = 0; i < rows; ++i)
            {
                for (var j = 0; j < rows-i-1; j++)
                {
                    switch (criterion)
                    {
                        case ComparisonCriterion.Max:
                            if (matrix[j].Max() > matrix[j + 1].Max() && order == Order.Descen ||
                            matrix[j].Max() < matrix[j + 1].Max() && order == Order.Ascen) 
                                Swap(ref matrix[j],ref matrix[j+1]);
                            break;
                        case ComparisonCriterion.Sum:
                            if (matrix[j].Sum() > matrix[j+1].Sum() && order == Order.Descen ||
                                matrix[j].Sum() < matrix[j+1].Sum() && order == Order.Ascen )
                                Swap(ref matrix[j],ref matrix[j+1]);
                            break;
                        case ComparisonCriterion.Min:
                            if (matrix[j].Min() < matrix[j+1].Min() && order == Order.Descen ||
                                matrix[j].Min() > matrix[j+1].Min() && order == Order.Ascen )
                                Swap(ref matrix[j],ref matrix[j+1]);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(criterion), criterion, null);
                    }
                }
            }
        }

        private static void Swap(ref int[] a, ref int[] b)
        {
            int[] t = a;
            a = b;
            b = t;
        }
    }
}