namespace CGE_1
{
    internal class Program
    {
        static async Task Main()
        {
            string path = @"C:\Users\sloni\OneDrive\Рабочий стол\vs\Тестовые задания\СПб ГАУ «Центр государственной экспертизы»\CGE\CGE_1\War_and_Peace.txt";
            SingleThreadSearch.Met(path);
            Console.WriteLine();
            await MultiThreadSearch.Met(path);
            Console.ReadLine();
        }
    }
}