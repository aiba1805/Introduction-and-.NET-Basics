using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;

namespace GC.Task1
{
    public class IntComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x == y)
            {
                return 0;
            }
            else if (x > y)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }

    public class SearchAlgorithm
    {
        public static int BinarySearch<T>(T[] arr, int l, int r,T x, IComparer<T> comparer)
        {
            if (r >= l) { 
                int mid = l + (r - l) / 2; 
                
                if (comparer.Compare(arr[mid],x) == 0) 
                    return mid; 

                return comparer.Compare(arr[mid],x) == 1 ? BinarySearch(arr, l, mid - 1, x,comparer) 
                    : BinarySearch(arr, mid + 1, r, x,comparer);
            } 
            
            return -1; 
        }
    }
}