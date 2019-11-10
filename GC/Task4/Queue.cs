using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using Microsoft.VisualBasic;

namespace GC.Task4
{
    public class Queue<T> : IEnumerable<T>
    {
        private T[] arr;
        private int _front;
        private int _rear;
        private int _max;

        public Queue(int size)
        {
            arr = new T[size];
            _front = 0;
            _rear = -1;
            _max = size;
        }

        public void Enqueue(T item)
        {
            if (_rear == _max - 1) throw new ArgumentException("Queue overflow");
            arr[++_rear] = item;
        }

        public T Dequeue()
        {
            if (_front == _rear + 1) throw new ArgumentException("Queue is empty");
            var p = arr[_front++];
            return p;
        }


        public IEnumerator<T> GetEnumerator()
        {
            for (var i = _front; i <= _rear; ++i)
            {
                yield return arr[i];
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = _front; i <= _rear; i++)
            {
                if (i == _rear) sb.Append(arr[i]);
                else sb.Append(arr[i]+" ");
            }
            return sb.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}