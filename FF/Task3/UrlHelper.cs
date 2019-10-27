using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.Task3
{
    public class UrlHelper
    {
        public static string AddOrChangeUrlParameter(string url, string keyValue)
        {
            var key = keyValue.Split("=")[0]; // достаем ключ 
            if(!url.Contains("?")) // если параметров нет
                return url + "?" + keyValue; // тогда возвращаем урл с единственным параметром
            var keyValues = url.Split("?")[1].Split("&").ToList(); // если параметров несколько достаем все ключи-значения
            foreach (var kv in keyValues)
            {
                var temp = kv.Split("=")[0]; // достаем ключ
                if (temp != key) continue; // если текущий ключ не равен тому который передали идем дальше
                var sb = new StringBuilder();
                var i = keyValues.IndexOf(kv); // берем индекс для обращения к нужному параметру
                keyValues[i] = keyValues[i].Replace(kv, keyValue); // меняем значение
                sb.AppendJoin("&", keyValues);
                return url.Split("?")[0] +"?"+ sb;
            }
            return url + "&" + keyValue;
        }
    }
}