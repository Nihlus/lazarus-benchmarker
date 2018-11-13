using System;
using System.Runtime.InteropServices;
using Lazarus.ADL;
using OpenTK;

namespace Lazarus.RW
{
    public class Lazarus
    {
        private static IntPtr[] EntryPoints = new IntPtr[2];
        private const string Library = "LZ.DLL";

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);
        
        public static void LoadEntryPoints()
        {
            var hmodule = LoadLibrary(Library);
            EntryPoints[0] = GetProcAddress(hmodule,"lzInvertMatrixByPtr");
            EntryPoints[1] = GetProcAddress(hmodule,"lzInvertMatrixByValue");
            //FreeLibrary(hmodule);
        }
        [Slot(0)]
        public static void InvertMatrixByPtr(ref Matrix2 matrix)
        {
            throw new BindingsNotRewrittenException();
        }
        [Slot(1)]
        public static Matrix2 InvertMatrixByValue(Matrix2 matrix)
        {
            throw new BindingsNotRewrittenException();
        }
    }
}