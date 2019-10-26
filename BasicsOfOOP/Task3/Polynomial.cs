using System;
using System.Linq;
using System.Text;

namespace BasicsOfOOP.Task3
{
    public class Polynomial
    {
        public int[] Coefficents { get; }

        public Polynomial(int[] coefficents)
        {
            this.Coefficents = coefficents;
        }

        public static Polynomial operator +(Polynomial rightOperand, Polynomial leftOperand)
        {
            int n = rightOperand.Coefficents.Length, m = leftOperand.Coefficents.Length;
            var sum = new int[n>m?n:m];
            for (var i = 0; i < n; i++) sum[i] += rightOperand.Coefficents[i];
            for (var i = 0; i < m; i++) sum[i] += leftOperand.Coefficents[i];
            return new Polynomial(sum);
        }
        
        public static Polynomial operator -(Polynomial rightOperand, Polynomial leftOperand)
        {
            var diff = new int[rightOperand.Coefficents.Length];
            for (var i = 0; i < rightOperand.Coefficents.Length; i++) 
            { 
                diff[i] = rightOperand.Coefficents[i] - leftOperand.Coefficents[i]; 
            }

            return new Polynomial(diff);
        }
        
        public static Polynomial operator *(Polynomial rightOperand, Polynomial leftOperand)
        {
            int n = rightOperand.Coefficents.Length, m = leftOperand.Coefficents.Length;
            var prod = new int[n+m-1];
            for (var i = 0; i < n; i++) 
            {
                for (var j = 0; j < m; j++)  
                { 
                    prod[i + j] += rightOperand.Coefficents[i] * leftOperand.Coefficents[j]; 
                } 
            } 

            return new Polynomial(RemoveZeros(prod));
        }
        
        public static (Polynomial,Polynomial) operator /(Polynomial rightOperand, Polynomial leftOperand)
        { 
            if (rightOperand.Coefficents.Length != leftOperand.Coefficents.Length) {
                throw new ArgumentException("Numerator and denominator vectors must have the same size");
            }
            var nd = PolyDegree(rightOperand.Coefficents);
            var dd = PolyDegree(leftOperand.Coefficents);
            if (dd < 0) throw new ArgumentException("Divisor must have at least one one-zero coefficient");
            if (nd < dd) throw new ArgumentException("The degree of the divisor cannot exceed that of the numerator");
            var n2 = new int[rightOperand.Coefficents.Length];
            rightOperand.Coefficents.CopyTo(n2, 0);
            var q = new int[rightOperand.Coefficents.Length];
            while (nd >= dd) {
                var d2 = PolyShiftRight(leftOperand.Coefficents, nd - dd);
                q[nd - dd] = n2[nd] / d2[nd];
                PolyMultiply(d2, q[nd - dd]);
                PolySubtract(n2, d2);
                nd = PolyDegree(n2);
            }
            return (new Polynomial(RemoveZeros(q)), new Polynomial(RemoveZeros(n2)));
        }

        private static int[] RemoveZeros(int[] arr)
        {
            return arr.Where(x => x != 0).ToArray();
        }

        private static int[] PolyShiftRight(int[] p, int places) {
            if (places <= 0) return p;
            var pd = PolyDegree(p);
            if (pd + places >= p.Length) {
                throw new ArgumentOutOfRangeException("The number of places to be shifted is too large");
            }
            var d = new int[p.Length];
            p.CopyTo(d, 0);
            for (var i = pd; i >= 0; --i) {
                d[i + places] = d[i];
                d[i] = 0;
            }
            return d;
        }
        
        
        private static int PolyDegree(int[] p) {
            for (int i = p.Length - 1; i >= 0; --i) {
                if (p[i] != 0.0) return i;
            }
            return int.MinValue;
        }
        private static void PolyMultiply(int[] p, int m) {
            for (var i = 0; i < p.Length; ++i) {
                p[i] *= m;
            }
        }

        private static void PolySubtract(int[] p, int[] s) {
            for (var i = 0; i < p.Length; ++i) {
                p[i] -= s[i];
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var pd = PolyDegree(Coefficents);
            for (var i = pd; i >= 0; --i) {
                var coeff = Coefficents[i];
                if (coeff == 0) continue;
                if (coeff == 1) {
                    if (i < pd) 
                        sb.Append(" + ");
                } 
                else if (coeff == -1) {
                    if (i < pd) 
                        sb.Append(" - ");
                    else 
                        sb.Append("-");
                } 
                else if (coeff < 0) {
                    if (i < pd) 
                        sb.Append(string.Format($" - {0:F1}", -coeff));
                    else 
                        sb.Append(string.Format($"{0:F1}", coeff));
                } 
                else {
                    if (i < pd) 
                        sb.Append(string.Format($" + {0:F1}", coeff));
                    else 
                        sb.Append(string.Format($"{0:F1}", coeff));
                }
                if (i > 1) sb.Append(string.Format($"x^{0}", i));
                else if (i == 1) sb.Append("x");
            }

            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Polynomial x)) return false;
            return x.Coefficents == this.Coefficents;
        }

        protected bool Equals(Polynomial other)
        {
            return Equals(Coefficents, other.Coefficents);
        }

        public override int GetHashCode()
        {
            return (Coefficents != null ? Coefficents.GetHashCode() : 0);
        }
    }
}