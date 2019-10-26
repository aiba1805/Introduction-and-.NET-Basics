using System;
using System.Collections.Generic;
using System.Text;

namespace OODP.Matrix
{
    public class MatrixBase
    {
        private List<List<int>> Matrix { get; set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Action<int> OnChange { get; set; }

        public virtual int this[int x, int y] {
            get
            {
                return Matrix[y][x];
            }

            set
            {
                Matrix[y][x] = value;
                if (OnChange != null)
                {
                    OnChange(value);
                }
            }
        }

        public MatrixBase(int width, int height)
        {
            Matrix = new List<List<int>>();
            Width = width;
            Height = height;

            for (var y = 0; y < Height; y++)
            {
                var line = new List<int>();
                for (var x = 0; x < Width; x++)
                {
                    line.Add(0);
                }

                Matrix.Add(line);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var y = 0; y < Height; y++)
            {
                var line = Matrix[y];
                sb.AppendLine(string.Join(" ", line));
            }

            return sb.ToString();
        }

        public static MatrixBase operator +(MatrixBase d1, MatrixBase d2)
        {
            if (d1.Width != d2.Width) throw new ArgumentException("Matrix should be the same size!");
            var result = new MatrixBase(d1.Width, d1.Height);

            for (var y = 0; y < d1.Height; y++)
            {
                for (var x = 0; x < d1.Width; x++)
                {
                    result[x, y] = d1[x, y] + d2[x, y];
                }
            }

            return result;
        }
    }
}