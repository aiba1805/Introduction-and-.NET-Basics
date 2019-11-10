using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FF.Task1;
using FF.Task3;
using FF.Task5;
using FF.Task8;
using NUnit.Framework;

namespace Tests.FF
{
    public class FFTest
    {
        [TestCase("Jeffrey Richter", 1000000, "+1 (425) 555-0100", "N", ExpectedResult = "Jeffrey Richter")]
        [TestCase("Jeffrey Richter", 1000000, "+1 (425) 555-0100", "NR", ExpectedResult =
            "Jeffrey Richter, 1,000,000.00")]
        [TestCase("Jeffrey Richter", 1000000, "+1 (425) 555-0100", "P", ExpectedResult = "+1 (425) 555-0100")]
        [TestCase("Jeffrey Richter", 1000000, "+1 (425) 555-0100", "F", ExpectedResult =
            "Jeffrey Richter, 1,000,000.00, +1 (425) 555-0100")]
        public string CustomerToString_Test(string name, decimal revenue, string phone, string format)
        {
            var c = new Customer
            {
                Name = name,
                Revenue = revenue,
                Phone = phone
            };
            var res = c.ToString(format);
            return res;
        }

        [TestCase("The In", "THE WIND IN THE WILLOWS", ExpectedResult = "The Wind in the Willows")]
        [TestCase("a an the of", "a clash of KINGS", ExpectedResult = "A Clash of Kings")]
        [TestCase("", "the quick brown fox", ExpectedResult = "The Quick Brown Fox")]
        public string TitleCase_Test(string minorWords, string convertWord)
        {
            var res = StringHelper.TitleCase(convertWord, minorWords);
            return res;
        }

        [TestCase("www.example.com", "key=value", ExpectedResult = "www.example.com?key=value")]
        [TestCase("www.example.com?key=value", "key2=value2", ExpectedResult = "www.example.com?key=value&key2=value2")]
        [TestCase("www.example.com?key=oldValue", "key=newValue", ExpectedResult = "www.example.com?key=newValue")]
        public string AddOrChangeUrlParameter_Test(string url, string keyValue)
        {
            var res = UrlHelper.AddOrChangeUrlParameter(url, keyValue);
            return res;
        }

        [TestCase("The greatest victory is that which requires no battle", ExpectedResult =
            "battle no requires which that is victory greatest The")]
        public string ReverseString_Test(string str)
        {
            var res = StringHelper.Reverse(str);
            return res;
        }

        [TestCase("10924817414294", "845327581979515", ExpectedResult = "856252399393809")]
        public string AddStringNumber_Test(string a, string b)
        {
            var res = StringHelper.AddStringNumbers(a, b);
            return res;
        }

        
    }
}