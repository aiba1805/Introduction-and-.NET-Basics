using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GC.Task6
{
    public class Set<T> : IEnumerable<T>
    {
        public T[] Items { get; private set; }

        public Set()
        {
            Items = new T[0];
        }
        
        
        public void Add(T item)
        {
            if (Items.Any(x => x.Equals(item))) return;
            var tempArr = new T[Items.Length+1];
            Items.CopyTo(tempArr,0);
            tempArr[^1] = item;
            Items = tempArr;
        }

        public void Remove(T item)
        {
            if (!Items.Any(x => x.Equals(item))) return;
            var tempArr = new T[Items.Length - 1];
            Items.Where(x => !x.Equals(item)).ToArray().CopyTo(tempArr, 0);
            Items = tempArr;
        }

        public bool Contains(T item)
        {
            return Items.Any(x => x.Equals(item));
        }

        public int Count()
        {
            return Items.Length;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var i in Items)
            {
                yield return i;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}