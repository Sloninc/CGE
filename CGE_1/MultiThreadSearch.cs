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
        /// <summary>
        /// определим многопоточный словарь для слов и их количества в исходном файле
        /// </summary>
        static ConcurrentDictionary<string, int> _dic = new ConcurrentDictionary<string, int>();
        /// <summary>
        /// Путь сохранения файла с информацией по количеству уникальных слов в исдном текстовом файле
        /// </summary>
        static string _outFile = @"C:\Users\sloni\OneDrive\Рабочий стол\vs\Тестовые задания\СПб ГАУ «Центр государственной экспертизы»\CGE\CGE_1\MOutSearch.txt";
        /// <summary>
        /// Метод многопоточного поиска, обработки и вывода информации по количеству уникальных слов в исдном текстовом файле
        /// </summary>
        /// <param name="path">место расположения исходного текстового файла</param>
        /// <returns></returns>
        public static async Task Search(string path)
        {
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                string[] str = File.ReadAllLines(path); // получаем массив строк исходного текстового файла
                // Многопоточный цикл обработки массива строк
                var rez = Parallel.For(0, str.Length, i =>
                {
                    // Получаем массив слов из исходного файла путем разделителя слов с помощью всех не алфавитно-цифровых символов
                    var words = Regex.Split(str[i].ToLower(), @"\W+");
                    foreach (var word in words)
                    {
                        if (word.Length > 3) // выбираем слова, длинна которых больше 3х букв
                        {
                            // добавляем в словарь уникальные слова и увеличиваем счетчик при повторных словах
                            _dic.AddOrUpdate(word, 1, (key, oldValue) => oldValue + 1); 
                        }
                    }
                });
                var text = _dic.OrderByDescending(x => x.Value).Where(v => v.Value > 5); // выбираем слова, количество которых больше 5ти
                StringBuilder stb = new StringBuilder();
                foreach (var item in text)
                {
                    stb.Append(string.Format("Слово: {0,-18}\t Количество повторов: {1}\n", item.Key, item.Value));
                }
                // Записываем результат в файл
                await File.WriteAllTextAsync(_outFile, stb.ToString());
                sw.Stop();
                var timer = sw.ElapsedMilliseconds;
                Console.WriteLine("Время, затраченное на подсчет слов в многопоточном методе: {0} миллисекунд", timer);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
