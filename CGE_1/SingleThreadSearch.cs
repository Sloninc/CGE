using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace CGE_1
{
    internal class SingleThreadSearch
    {
        static string _outFile = @"C:\Users\sloni\OneDrive\Рабочий стол\vs\Тестовые задания\СПб ГАУ «Центр государственной экспертизы»\CGE\CGE_1\SOutSearch.txt";
        public static void Met(string path)
        {
            Stopwatch sw = Stopwatch.StartNew();
            string str = File.ReadAllText(path);
            var words = Regex.Split(str.ToLower(), @"\W+")
                .Where(e => e.Length > 3)
                .GroupBy(g => g)
                .Select(s => new { Word = s.Key, Count = s.Count() })
                .OrderByDescending(z => z.Count)
                .Where(v => v.Count > 5);
            StringBuilder stb = new StringBuilder();
            foreach (var item in words)
            {
                stb.Append(string.Format("Слово: {0,-18}\t Количество повторов: {1}\n", item.Word, item.Count));
            }
            File.WriteAllText(_outFile, stb.ToString());
            sw.Stop();
            var timer = sw.ElapsedMilliseconds;    
            Console.WriteLine("Время, затраченное на подсчет слов в однопоточном методе: {0} миллисекунд", timer);
        }
    }
}
