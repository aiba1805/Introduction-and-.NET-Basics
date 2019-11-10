using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GC.Task5
{
    public class Stack<T> : IEnumerable<T>
    {
        private readonly T[] _arr;
        private int _top;
        private readonly int _max;

        public Stack(int size)
        {
            _arr = new T[size];
            _top = -1;
            _max = size;
        }

        public void Push(T item)
        {
            if (_top == _max - 1) throw new ArgumentException("Stack overflow");
            _arr[++_top] = item;
        }

        public T Pop()
        {
            if (_top == -1) throw new ArgumentException("Stack is empty");
            return _arr[_top--];
        }

        public T Peek()
        {
            if (_top == -1) throw new ArgumentException("Stack is empty");
            return _arr[_top];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) _arr).GetEnumerator();
        }

        public override string ToString()
        {
            if (_top == -1) throw new ArgumentException("Stack is empty");
            var sb = new StringBuilder();
            for (var i = 0; i <= _top; i++)
            {
                if (i == _top) sb.Append(_arr[i]);
                else sb.Append(_arr[i]+" ");
            }

            return sb.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
