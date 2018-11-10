using System;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

class Program
{
    private static CustomAttribute attr;

    public static void Main(string[] args)
    {
        if (File.Exists(args[0] + ".bak"))
            File.Delete(args[0] + ".bak");
        File.Copy(args[0], args[0] + ".bak");
        var fs = File.Open(args[0], FileMode.Open, FileAccess.ReadWrite);
        var def = ModuleDefinition.ReadModule(fs);
        var type = def.GetType("Lazarus.RW", "Lazarus");
        attr = new CustomAttribute(def.GetType("OpenTK", "RewrittenAttribute").GetConstructors().First());
        Process(type.Methods.FirstOrDefault(x => x.Name == "InvertMatrixByPtr"));
        Process(type.Methods.FirstOrDefault(x => x.Name == "InvertMatrixByValue"));
        Console.WriteLine("INFO: Writing rewritten assembly...");
        def.Write(fs);
        fs.Flush();
        fs.Close();
    }

    static void Process(MethodDefinition method)
    {
        ILProcessor il = method?.Body.GetILProcessor();
        il.Body.Instructions.Clear();
        var entryPointsField = method.DeclaringType.Fields.FirstOrDefault(x => x.Name == "EntryPoints");
        var slot = (int) method.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name == "SlotAttribute")
            .ConstructorArguments[0].Value;
        Console.WriteLine("INFO: Starting to rewrite " + method.Name + " at slot " + slot);
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Ldsfld, entryPointsField);

        il.Emit(OpCodes.Ldc_I4, slot);
        il.Emit(OpCodes.Ldelem_I);

        // issue calli
        var signature = new CallSite(method.ReturnType)
        {
            CallingConvention = MethodCallingConvention.StdCall,
        };

        foreach (var p in method.Parameters)
        {
            signature.Parameters.Add(p);
        }

        // Since the last parameter is always the entry point address,
        // we do not need any special preparation before emitting calli.
        il.Emit(OpCodes.Calli, signature);
        il.Emit(OpCodes.Ret);
        method.CustomAttributes.Add(attr);
        Console.WriteLine("INFO: Complete");
    }
}