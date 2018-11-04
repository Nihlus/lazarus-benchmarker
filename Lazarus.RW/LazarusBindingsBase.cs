using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Runtime.InteropServices;
using OpenTK;

namespace Lazarus.RW
{
    public class LazarusBindingsBase : BindingsBase
    {
        public Dictionary<string,IntPtr> Addresses { get; set; } = new Dictionary<string, IntPtr>();
        protected override IntPtr GetAddress(string funcname)
        {
            return Addresses.ContainsKey(funcname) ? Addresses[funcname] : IntPtr.Zero;
        }

        protected override object SyncRoot { get; }
        internal override void LoadEntryPoints()
        {
            var handle = Kernel32.GetModuleHandle("lz.dll");
            Addresses.Add("lzInvertMatrixByPtr",Kernel32.GetProcAddress(handle,"lzInvertMatrixByPtr"));
            Addresses.Add("lzInvertMatrixByValue",Kernel32.GetProcAddress(handle,"lzInvertMatrixByValue"));
        }

    }
        public class Kernel32
        {
            public static IntPtr LazarusHandle { get; set; }

            [DllImport("kernel32.dll")]
            public static extern IntPtr GetModuleHandle(string lpModuleName);
        		
            [DllImport("kernel32.dll")]
            public static extern IntPtr GetProcAddress(IntPtr hMod, string proc);
        }
}