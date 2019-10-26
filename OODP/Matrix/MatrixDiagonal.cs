namespace OODP.Matrix
{
    public class MatrixDiagonal : MatrixSquare
    {
        public MatrixDiagonal(int size) : base(size)
        {

        }

        public int this[int x] {
            get => base[x, x];
            set => base[x, x] = value;
        }
    }
}