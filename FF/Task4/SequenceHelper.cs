using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FF.Task4
{
    public class SequenceHelper
    {
        public static List<T> UniqueInOrder<T>(List<T> list)
        {
            var distinctOrder = new List<T> {list[0]};
            var index = 0;
            for (int i = 0; i < list.Count; index++)
            {
                if (index >= distinctOrder.Count) return distinctOrder;
                for (int j = i+1; j < list.Count; j++)
                {
                    if (distinctOrder[index].Equals(list[j])) continue;
                    distinctOrder.Add(list[j]);
                    i = j;
                    break;
                }
            }

            return distinctOrder;
        }
    }
}