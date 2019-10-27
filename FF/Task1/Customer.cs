using System;
using System.Globalization;
using Microsoft.VisualBasic;

namespace FF.Task1
{
    public class Customer
    {

        public string Name { get; set; }
        public decimal  Revenue { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return this.ToString("F");
        }

        public string ToString(string format)
        {
            if (string.IsNullOrEmpty(format)) format = "F";

            return format.ToUpperInvariant() switch
            {
                "F" => Strings.Format($"{Name}, {Revenue:N}, {Phone}"),
                "P" => Strings.Format($"{Phone}"),
                "NR" => Strings.Format($"{Name}, {Revenue:N}"),
                "R" => Strings.Format($"{Revenue:N}"),
                "N" => Strings.Format($"{Name}"),
                _ => throw new FormatException(Strings.Format("The {0} format string is not supported.", format))
            };
        }
    }
}