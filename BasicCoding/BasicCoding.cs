using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualBasic;

namespace BasicCoding
{
    public class Tasks
    {
        static void Main(string[] args)
        {
            int[] arr = new[] {4, 5, 22, 12, 3, 34, 56, 99};
            Console.WriteLine("Массив:");
            arr.ToList().ForEach(x => Console.Write($"{x} "));
            Console.WriteLine($"\nМаксимальный элемент массива: {Max(arr, arr.Length)}");
        }
        
        /// <summary>
        ///  Метод вставляет биты первого числа с j по i в биты второго числа с j по i.
        /// С помощью методов класса Convert конвертируем число в двоичное значение в виде строки,
        /// а затем в массив, затем переворачиваем его так как, в двоичном исчислении нумерация начинается справа,
        /// Затем обратно в массив, чтобы можно было с ним работать.
        /// Далее следует цикл, который соотвественно меняет биты.
        /// </summary>
        /// <param name="numberSource"></param>
        /// <param name="numberIn"></param>
        /// <param name="j"></param>
        /// <param name="i"></param>
        /// <returns> конвертируем массив символов перевернутый, преобразованный в строку, в качестве системы измерения задаем двоичную </returns>
        public static int InsertNumber(int numberSource, int numberIn, int j, int i)
        {
            var sourceString = Convert.ToString(numberSource, 2)
                .ToCharArray().Reverse().ToArray();
            var inString = Convert.ToString(numberIn, 2)
                .ToCharArray().Reverse().ToArray();
            for (int k = j; k <= i; k++)
            {
                inString[k] = sourceString[k];
            }

            return Convert.ToInt32(new string(inString.Reverse().ToArray()), 2);
        }

        /// <summary>
        /// Если индекс равен 1, то возращаем первый элемент массива.
        /// Затем рекурсивно вызываем функцию и передаем туда индекс на один меньше.
        /// Сравниваем на большое значение элемент который мы получили из рекурсивного вызова и и текущий элемент.
        /// </summary>
        /// <param name="arr">Массив чисел</param>
        /// <param name="n">Индекс</param>
        /// <returns>Возращает максимальное значение</returns>
        public static int Max(int[] arr, int n)
        {
            if (n is 1)
                return arr[0];
            var max = Max(arr, n-1);
            return arr[n-1] > max ? arr[n-1] : max;
        }
        
        /// <summary>
        /// Проходимся по массиву с помощью foreach, для каждого значения 
        /// получаем индекс с помощью LINQ функции IndexOf, пред этим конвертируем
        /// массив в лист.
        /// Считаем сумму до и после индекса, выбирая элементы с помощью LINQ функции Where
        /// Сравниваем суммы.
        /// </summary>
        /// <param name="arr">Массив вещественных чисел</param>
        /// <returns>Возращает целое число, индекс массива, если такого числа нет возвращает 1.</returns>
        public static int FindByEqualSum(float[] arr)
        {
            foreach (var i in arr)
            {
                var indexOfCur = arr.ToList().IndexOf(i);
                var sumBefore = arr.Where(x => arr.ToList().IndexOf(x) < indexOfCur).Sum();
                var sumAfter = arr.Where(x => arr.ToList().IndexOf(x) > indexOfCur).Sum();
                if (sumBefore == sumAfter)
                    return indexOfCur;
            }

            return 1;
        }

        /// <summary>
        /// Проверяем строки на содержание только латинских букв.
        /// Анонимно создаем новую строку из строки у которой с помощью функции Distinct
        /// удаляем повторяющиеся элементы.
        /// С помощью перегурженного оператора + складываем строки 
        /// </summary>
        /// <param name="first">Первая строка</param>
        /// <param name="second"Вторая строка</param>
        /// <returns></returns>
        public static string Concat(string first, string second)
        {
            if(!(CheckerAZ(first) && CheckerAZ(second))) throw new ArgumentException("String contains non alphabetical characters!");
            return new string(first.Distinct().ToArray())
                + new string(second.Distinct().ToArray());
        }
        /// <summary>
        /// Функция которая проверяет содержит ли строка
        /// только латинские букву в противном случае возвращает false.
        /// </summary>
        /// <param name="str">проверяемая строка</param>
        /// <returns></returns>
        private static bool CheckerAZ(string str)
        {
            foreach (var i in str)
            {
                var c = Char.ToLower(i);
                if (!(c >= 'a' && c <= 'z'))
                    return false;
            }

            return true;
        }

        public static (int,Stopwatch) FindNextBiggerNumber(int num)
        {
            var s = new Stopwatch(); // Инициализируем таймер
            s.Start(); // Запускаем
            var digits = IntToDigits(num); // Функция разделения числа на цифры
             // Отнимаем от длины массива цифр 2,
             // так как число должно быть хотя бы двухзначным
            var j = digits.Length - 2;
             // Проверяем если даже число двухзначное,
             // но текущая цифра больше либо равна следующей
            while (j != -1 && digits[j] >= digits[j + 1]) --j;
            if (j == -1) // если текущий индекс числа -1 то возвращаем что -1 
                return (-1,s); 
            var k = digits.Length - 1; // индекс последней цифры
            while (digits[j] >= digits[k]) --k; // если текущая цифра больше последней, проходимся справа налево
            Swap(ref digits, j, k); // Меняем местами 
            int l = j + 1, r = digits.Length -1; // l - следующая цифра после текущей, r - последняя 
            while (l<r) // пока слева больше, менять местами
                Swap(ref digits, l++, r--);
            s.Start();
            return (DigitsToInt(digits),s);
        }

        private static int[] IntToDigits(int num)
        {
            var digits = new List<int>();
            foreach (var c in num.ToString().ToArray())
            {
                Int32.TryParse(c.ToString(), out int value);
                digits.Add(value);
            }

            return digits.ToArray();
        }

        private static int DigitsToInt(int[] arr)
        {
            int value = 0;
            for (int i = 0; i < arr.Length; ++i)
            {
                value *= 10;
                value += arr[i];
            }

            return value;
        }

        private static void Swap<T>(ref T[] arr, int i, int j)
        {
            T c = arr[i];
            arr[i] = arr[j];
            arr[j] = c;
        }

        public static int[] FilterDigit(int number, int[] arr)
        {
            var result = new List<int>();
            foreach (var num in arr)
            {
                foreach (var digit in IntToDigits(num))
                {
                    if(number == digit && !result.Contains(num)) result.Add(num);
                }
            }

            return result.ToArray();
        }
    }
}