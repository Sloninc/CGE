using System.Reflection;
using System.Reflection.Emit;

namespace CGE_2
{
    public delegate Cat CatCreator(int i, string s, int t);
    internal class Program
    {
        static void Main()
        {
            Type type = Type.GetType("CGE_2.Cat");
             var f = type.GetFields();

            var assembly = new AssemblyName("DynamicCGE_2");
            AssemblyBuilder ab = AssemblyBuilder.DefineDynamicAssembly(assembly, AssemblyBuilderAccess.Run);

            ModuleBuilder mb = ab.DefineDynamicModule(assembly.Name);

            TypeBuilder tb = mb.DefineType("InstCat", TypeAttributes.Public, type);

            Type[] parameterTypes = { typeof(int), typeof(string), typeof(int) };

            ConstructorBuilder ctor = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, parameterTypes);
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
            Type? t = tb.CreateType();

            Cat cat = null;
            if (t != null)
                cat = (Cat)Activator.CreateInstance(t, new object[] { 1, "Tom", 2 });
            Console.WriteLine(cat.age + " " + cat.name + " " + cat.tailCount + " " + cat.GetType().BaseType);
            Console.ReadLine();
        }
    }
}