using System;
using System.IO;
using System.Linq;

namespace GC.Task2
{
    public class TextFileHelper
    {
        public static int WordFrequency(string filename,string word)
        {
            string textFromFile;
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                var array = new byte[stream.Length];
                stream.Read(array, 0, array.Length);
                textFromFile= System.Text.Encoding.Default.GetString(array);
            }

            return textFromFile.Split(" ")
                .Count(x => x.ToLower().StartsWith(word.ToLower()));
        }
    }
}