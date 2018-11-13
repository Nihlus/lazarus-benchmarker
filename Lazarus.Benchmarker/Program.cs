using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using AdvancedDLSupport;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Lazarus.ADL;
using Lazarus.RW;

namespace Lazarus.Benchmarker
{
    public class Program
    {
        private const int Calls = 1000;

        #region benchmark

        [CoreJob,ClrJob,MonoJob()]
        [RankColumn]
        public class InvertMatrixByPtrBenchmark
        {
            private ILazarus _lazarus;
            private Matrix2 _matrix;

            [Params("ADL, calli", "ADL, delegates", "Rewritten")]
            public string Method;

            [GlobalSetup]
            public void Setup()
            {
                _matrix = new Matrix2()
                    {Row0 = new Vector2() {X = 1f, Y = 0f}, Row1 = new Vector2() {X = 10f, Y = 5f}};
                if (!File.Exists("lz.dll"))
                using (var stream = Assembly.GetAssembly(typeof(ILazarus))
                    .GetManifestResourceStream("Lazarus.ADL." + (Environment.Is64BitProcess ? "lz64" : "lz32") +
                                               ".dll"))
                {
                    using (var output = File.OpenWrite("lz.dll"))
                    {
                        stream.CopyTo(output);
                        output.Flush();
                    }
                }

                if (Method == "ADL, delegates")
                    _lazarus = NativeLibraryBuilder.Default.ActivateInterface<ILazarus>("lz.dll");
                else if (Method == "ADL, calli")
                    _lazarus = new NativeLibraryBuilder(ImplementationOptions.UseIndirectCalls |
                                                        ImplementationOptions.EnableDllMapSupport |
                                                        ImplementationOptions.EnableOptimizations |
                                                        ImplementationOptions.SuppressSecurity)
                        .ActivateInterface<ILazarus>("lz.dll");
                else if (Method == "Rewritten")
                {
                    RW.Lazarus.LoadEntryPoints();
                }
            }

            [Benchmark]
            public void InvertMatrixByPtr()
            {
                    if (Method == "Rewritten")
                        RW.Lazarus.InvertMatrixByPtr(ref _matrix);
                    else
                        _lazarus.InvertMatrixByPtr(ref _matrix);
            }
        }

        
        [CoreJob,CoreRtJob,ClrJob,MonoJob()]
        [RankColumn]
        public class InvertMatrixByValueBenchmark
        {
            private ILazarus _lazarus;
            private Matrix2 _matrix;

            [Params("ADL, calli", "ADL, delegates", "Rewritten")]
            public string Method;

            [GlobalSetup]
            public void Setup()
            {
                _matrix = new Matrix2()
                    {Row0 = new Vector2() {X = 1f, Y = 0f}, Row1 = new Vector2() {X = 10f, Y = 5f}};
                ;
                if (!File.Exists("lz.dll"))
                using (var stream = Assembly.GetAssembly(typeof(ILazarus))
                    .GetManifestResourceStream("Lazarus.ADL." + (Environment.Is64BitProcess ? "lz64" : "lz32") +
                                               ".dll"))
                {
                    using (var output = File.OpenWrite("lz.dll"))
                    {
                        stream.CopyTo(output);
                        output.Flush();
                    }
                }

                if (Method == "ADL, delegates")
                    _lazarus = NativeLibraryBuilder.Default.ActivateInterface<ILazarus>("lz.dll");
                else if (Method == "ADL, calli")
                    _lazarus = new NativeLibraryBuilder(ImplementationOptions.UseIndirectCalls |
                                                        ImplementationOptions.EnableDllMapSupport |
                                                        ImplementationOptions.EnableOptimizations |
                                                        ImplementationOptions.SuppressSecurity)
                        .ActivateInterface<ILazarus>("lz.dll");
                else if (Method == "Rewritten")
                {
                    RW.Lazarus.LoadEntryPoints();
                }
            }

            [Benchmark]
            public void InvertMatrixByValue()
            {
                    if (Method == "Rewritten")
                        RW.Lazarus.InvertMatrixByValue(_matrix);
                    else
                        _lazarus.InvertMatrixByValue(_matrix);
            }
        }
        #endregion

        static void Main(string[] args)
        {
            BenchmarkRunner.Run<InvertMatrixByPtrBenchmark>();
            BenchmarkRunner.Run<InvertMatrixByValueBenchmark>();
        }
    }
}