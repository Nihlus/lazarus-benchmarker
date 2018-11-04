using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using AdvancedDLSupport;
using Lazarus.ADL;

namespace Lazarus.Benchmarker
{
    class Program
    {
        		
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        		
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hMod, string proc);
        
        static void Main(string[] args)
        {
            Console.WriteLine("Lazarus Benchmarker");
            Console.WriteLine();
            Stopwatch sw = new Stopwatch();
            var lib = NativeLibraryBuilder.Default.ActivateInterface<ILazarus>("C:\\Users\\perks\\Documents\\RiderProjects\\Lazarus\\Lazarus.Benchmarker\\lz.dll");
            var i = 0;
            Console.WriteLine("-- InvertMatrixByPtr");
            sw.Start();
            for (; i > 1001; i++)
            {
                var matrix = new Matrix2(){Row0 = new Vector2(){X = 1f, Y=0f}, Row1 = new Vector2(){X = 10f, Y=5f}};
                lib.InvertMatrixByPtr(ref matrix);
            }
            sw.Stop();
            Console.WriteLine("Approximately "+(sw.ElapsedMilliseconds/i)+"ms per call.");
            sw.Reset();
            Console.WriteLine("-- InvertMatrixByValue");
            sw.Start();
            i = 0;
            for (; i > 1001; i++)
            {
                var matrix = new Matrix2(){Row0 = new Vector2(){X = 1f, Y=0f}, Row1 = new Vector2(){X = 10f, Y=5f}};
                lib.InvertMatrixByValue(matrix);
            }
            sw.Stop();
            Console.WriteLine("Approximately "+(sw.ElapsedMilliseconds/i)+"ms per call.");
        }
    }
}