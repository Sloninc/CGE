using Faker;
namespace CGE_4
{
    internal class Program
    {
        static void Main()
        {
            var count = 0;  //с какой попытки Маша вытаскивает нужные веса
            bool stop = true;
            var WightPlates = RandomNumber.Next(25, 50);  //количество блинов в куче
            Stack<int> StackWightPlates = new Stack<int>(WightPlates); //куча наваленных блинов друг на друга
            for(int i=0; i<WightPlates; i++)
            {
                StackWightPlates.Push(RandomNumber.Next(1, 16)); // случайный вес блина в диапазоне от 1кг до 16кг
            }
            List<int> ListWightPlates = new List<int>();
            int temp = 0;
            while (stop)
            {
                count++;    
                temp = StackWightPlates.Pop();
                if(temp == 16)
                {
                    Console.WriteLine("Найден пудовый блин, отобрав из кучи {0} блинов", count);
                    stop = false;
                }
                foreach(int i in ListWightPlates)
                {
                    if (temp + i == 16)
                        stop = false;
                }
                ListWightPlates.Add(temp);
            }
            if (stop)
                Console.WriteLine("Блины, дающие в сумме 16кг не найдены");
            else
                Console.WriteLine("Найдены два блина {0}кг и {1}кг в сумме 16кг, отобрав из кучи {2} блинов", temp, 16 - temp, count);
            Console.ReadLine();
        }
    }
}