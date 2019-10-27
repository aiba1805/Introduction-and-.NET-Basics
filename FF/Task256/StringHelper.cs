using System;
using System.Linq;
using System.Text;

namespace FF.Task5
{
    public class StringHelper
    {
        public static string AddStringNumbers(string a, string b)
        {
            long.TryParse(a, out var numA);
            long.TryParse(b, out var numB);
            return (numA + numB).ToString();
        }

        public static string Reverse(string toReverse)
        {
            var sb = new StringBuilder();
            return sb.AppendJoin(" ",toReverse.Split(" ").Reverse()).ToString();
        }
        
        public static string TitleCase(string toBeConverted, string minorWords = "")
        {
            var convertList = toBeConverted.Split(" ");
            var sb = new StringBuilder();
            sb.Append(ConvertToTitleCase(convertList[0]));
            for (var i = 1; i < convertList.Length; ++i)
            {
                if (minorWords != "")
                {
                    if (minorWords.Split(" ").ToList().Any(j =>
                        string.Equals(convertList[i], j, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        sb.Append(" " + convertList[i].ToLower());
                    }
                    else sb.Append(" " + ConvertToTitleCase(convertList[i]));
                }
                else sb.Append(" " + ConvertToTitleCase(convertList[i]));
            }

            return sb.ToString();
        }

        private static string ConvertToTitleCase(string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input.First().ToString().ToUpper() + input.Substring(1).ToLower()
            };
    }
}