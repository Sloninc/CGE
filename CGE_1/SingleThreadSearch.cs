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
        /// <summary>
        /// Путь сохранения файла с информацией по количеству уникальных слов в исдном текстовом файле
        /// </summary>
        static string _outFile = @"C:\Users\sloni\OneDrive\Рабочий стол\vs\Тестовые задания\СПб ГАУ «Центр государственной экспертизы»\CGE\CGE_1\SOutSearch.txt";
        /// <summary>
        /// Метод однопоточного поиска, обработки и вывода информации по количеству уникальных слов в исдном текстовом файле
        /// </summary>
        /// <param name="path">место расположения исходного текстового файла</param>
        public static void Search(string path)
        {
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                // получаем текст из исходного файла в виде строки
                string str = File.ReadAllText(path);
                // из исходного текста делаем выборку слов
                var words = Regex.Split(str.ToLower(), @"\W+") // разделитель слов с помощью всех не алфавитно-цифровых символов
                    .Where(e => e.Length > 3) // выбираем слова, длинна которых больше 3х букв
                    .GroupBy(g => g)
                    .Select(s => new { Word = s.Key, Count = s.Count() })
                    .OrderByDescending(z => z.Count)
                    .Where(v => v.Count > 5); // выбираем слова, количество которых больше 5ти
                StringBuilder stb = new StringBuilder();
                foreach (var item in words)
                {
                    stb.Append(string.Format("Слово: {0,-18}\t Количество повторов: {1}\n", item.Word, item.Count));
                }
                // Записываем результат в файл
                File.WriteAllText(_outFile, stb.ToString());
                sw.Stop();
                var timer = sw.ElapsedMilliseconds;
                Console.WriteLine("Время, затраченное на подсчет слов в однопоточном методе: {0} миллисекунд", timer);
            }
           catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
