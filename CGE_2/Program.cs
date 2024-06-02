using System.Reflection;
using System.Reflection.Emit;

namespace CGE_2
{
    public delegate Cat CatCreator(int i, string s, int t);
    internal class Program
    {
        static void Main()
        {
            try
            {
                // получаем метаописание исходного абстрактного класса
                Type type = Type.GetType("CGE_2.Cat");
                // получаем метаописание полей исходного абстрактного класса
                var f = type.GetFields();
                // Определим метаописание сборки, в котором будет создаваться рантайм тип и его экземпляр
                var assembly = new AssemblyName("DynamicCGE_2");
                AssemblyBuilder ab = AssemblyBuilder.DefineDynamicAssembly(assembly, AssemblyBuilderAccess.Run);
                // Определим метаописание модуля, в котором будет создаваться рантайм тип и его экземпляр
                ModuleBuilder mb = ab.DefineDynamicModule(assembly.Name);
                // Определим метаописание рантайм типа, который наследуется от исходного абстрактного класса
                TypeBuilder tb = mb.DefineType("InstCat", TypeAttributes.Public, type);
                // Определим метаописание параметров для конструктора экземпляра рантайм типа
                Type[] parameterTypes = { typeof(int), typeof(string), typeof(int) };
                // Определим метаописание конструктора экземпляра рантайм типа
                ConstructorBuilder ctor = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, parameterTypes);
                // Получаем экземпляр кодогенератора наи языке MSIL и задаем команды для него
                ILGenerator ctorIL = ctor.GetILGenerator();
                ctorIL.Emit(OpCodes.Ldarg_0);
                ctorIL.Emit(OpCodes.Ldarg_1);
                ctorIL.Emit(OpCodes.Stfld, f[0]);
                ctorIL.Emit(OpCodes.Ldarg_0);
                ctorIL.Emit(OpCodes.Ldarg_2);
                ctorIL.Emit(OpCodes.Stfld, f[1]);
                ctorIL.Emit(OpCodes.Ldarg_0);
                ctorIL.Emit(OpCodes.Ldarg_3);
                ctorIL.Emit(OpCodes.Stfld, f[2]);
                ctorIL.Emit(OpCodes.Ret);
                // Получаем метаописание рантайм типа
                Type? t = tb.CreateType();
                Cat cat = null;
                if (t != null)
                    // создаем экземпляр рантайм типа
                    cat = (Cat)Activator.CreateInstance(t, new object[] { 1, "Tom", 2 });
                Console.WriteLine("Инициированные поля абстрактного класса Cat:\nage = {0}\nname = {1}\ntailCount = {2}\n",cat.age, cat.name, cat.tailCount);
                Console.WriteLine("Вывод типа экземпляра абстактного класса: {0}", cat.GetType().BaseType);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}