using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Concurrent;

namespace CGE_1
{
    internal class MultiThreadSearch
    {
        static ConcurrentDictionary<string, int> _dic = new ConcurrentDictionary<string, int>();
        static string _outFile = @"C:\Users\sloni\OneDrive\Рабочий стол\vs\Тестовые задания\СПб ГАУ «Центр государственной экспертизы»\CGE\CGE_1\MOutSearch.txt";
        public static void Met(string path)
        {
            Stopwatch sw = Stopwatch.StartNew();
            string[] str = File.ReadAllLines(path);
            var rez = Parallel.For(0, str.Length, i =>
            {
                var words = Regex.Split(str[i].ToLower(), @"\W+");
                foreach (var word in words)
                {
                    if (word.Length > 3)
                    {
                        _dic.AddOrUpdate(word, 1, (key, oldValue) => oldValue + 1);
                    }
                }
            });
            var text = _dic.OrderByDescending(x => x.Value).Where(v => v.Value > 5);
            StringBuilder stb = new StringBuilder();
            foreach (var item in text)
            {
                stb.Append(string.Format("Слово: {0,-18}\t Количество повторов: {1}\n", item.Key, item.Value));
            }
            File.WriteAllText(_outFile, stb.ToString());
            sw.Stop();
            var timer = sw.ElapsedMilliseconds;
            Console.WriteLine("Время, затраченное на подсчет слов в многопоточном методе: {0}", timer);
        }
    }
}
