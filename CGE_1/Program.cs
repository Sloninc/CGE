namespace CGE_1
{
    internal class Program
    {
        static void Main()
        {
            string path = @"C:\Users\sloni\OneDrive\Рабочий стол\vs\Тестовые задания\СПб ГАУ «Центр государственной экспертизы»\CGE\CGE_1\War_and_Peace.txt";
            SingleThreadSearch.Met(path);
            Console.WriteLine();
            MultiThreadSearch.Met(path);
            Console.ReadLine();
        }
    }
}