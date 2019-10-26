using System;
using System.Text.RegularExpressions;

namespace BasicsOfOOP.Task2
{
    public class Circle: IShape
    {
        public double Radius { get; set; }

        public double Area()
        {
            return Math.PI * Math.Pow(2, Radius);
        }

        public double Perimeter()
        {
            return 2 * Math.PI * Radius;
        }
    }
}