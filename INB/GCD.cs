using System;
using System.Diagnostics;

namespace INB
{
    public class GCD
    {
        private static int EwclideGCD(int a, int b)
        {
            if (a == b) return a;
            if (a == 0) return b;
            if (b == 0) return a;
            a = a > 0 ? a : a * -1;
            b = b > 0 ? b : b * -1;
            while (a != b)
            {
                if (a > b)
                {
                    a = a - b;
                }
                else
                {
                    b = b - a;
                }
            }
            return a;
        }
        
        private static int EwclideGCD(params int[] arg) 
        {
            int t = arg[0];
            foreach (var item in arg)
            {
                t = EwclideGCD(t, item);
            }
            return t;
        }

        public static (int,TimeSpan) EwclideGcdTime(int a, int b)
        {
            var stopwatch = Stopwatch.StartNew();
            var res = EwclideGCD(a, b);
            stopwatch.Stop();
            return (res,stopwatch.Elapsed);
        }
        
        public static (int,TimeSpan) EwclideGcdTime(params int[] arg)
        {
            var stopwatch = Stopwatch.StartNew();
            var res = EwclideGCD(arg);
            stopwatch.Stop();
            return (res,stopwatch.Elapsed);
        }
        
        private static int SteineGcd(int a,int b)
        {
            if (a == b) return a;
            if (a == 0) return b;
            if (b == 0) return a;
            if ((a & 1)==0) 
            {
                if ((b & 1)==1) return SteineGcd(a/2, b); 
                else return SteineGcd(a/2, b/2) * 2;     
            }

            if ((b & 1)==0) return SteineGcd(a, b/2);

            if (a > b) return SteineGcd((a - b)/2, b);

            return SteineGcd((b - a)/2 , a);
        }

        private static int SteineGcd(params int[] arg)
        {
            if (arg.Length == 1) return arg[0];
            int temp = arg[0];
            foreach (var item in arg)
            {
                temp = SteineGcd(temp, item);
            }
            return temp;
        }
        
        public static (int,TimeSpan) SteineGcdTime(int a, int b)
        {
            var stopwatch = Stopwatch.StartNew();
            var res = SteineGcd(a, b);
            stopwatch.Stop();
            return (res,stopwatch.Elapsed);
        }
        
        public static (int,TimeSpan) SteineGcdTime(params int[] arg)
        {
            var stopwatch = Stopwatch.StartNew();
            var res = SteineGcd(arg);
            stopwatch.Stop();
            return (res,stopwatch.Elapsed);
        }
    }
}