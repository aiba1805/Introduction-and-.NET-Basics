using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INB
{
    public class MethodDetails
    {
        public static  string  ConvertDoubleToBinaryString(double number)
        {
            unsafe
            {
                long value = *((long*) &number); //конвертируем переменную Double по средством явного преобразования адреса переменной
                byte[] bytes = new byte[8]; // создаем массив байтов
                fixed(byte* b = bytes) // фиксируем (защищаем от сборщика мусора) 
                    *((long*)b) = value; // явное преобразование значения Long в массив байтов
                string result = String.Empty;
                List<string> bits = new List<string>();
                for (int i = 0; i < bytes.Length; i++)
                    bits.Add(ConvertByteToBitString(bytes[i])); 
                bits.Reverse(); // Переворачиваем байты
                foreach (var str in bits)
                    result += str;
                return result;
            }
        }
        private static string ConvertByteToBitString(byte b)
        {
            StringBuilder str = new StringBuilder(8);
            int[] bl  = new int[8];

            for (int i = 0; i < bl.Length; i++)
            {               
                bl[bl.Length - 1 - i] = ((b & (1 << i)) != 0) ? 1 : 0; // конвертируем байт в биты
            }

            foreach ( int num in bl) str.Append(num);

            return str.ToString();
        }
    }

    public static class NullableExtension
    {
        public static bool IsNull(this object? value)
        {
            if (value != null)
                return false;
            return true;
        }
    }
}