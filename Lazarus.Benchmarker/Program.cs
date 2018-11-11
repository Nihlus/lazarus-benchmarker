using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using AdvancedDLSupport;
using Lazarus.ADL;

namespace Lazarus.Benchmarker
{
    class Program
    {
        private const long Calls = 100000000;
        private const long Tests = 1;


        static void Main(string[] args)
        {
            var sb = new StringBuilder();
            Console.WriteLine("Lazarus Benchmarker");
            Console.WriteLine();
            Console.WriteLine("-=-= ADL/D =-=-");
            Console.WriteLine();
            Stopwatch sw = new Stopwatch();
            var lib = NativeLibraryBuilder.Default.ActivateInterface<ILazarus>("lz.dll");
            var i = 0;
            Console.WriteLine("-- InvertMatrixByPtr");
            var results = new List<double>();
            for (var j = 0; j < Tests; j++)
            {
                sw.Start();
                while (i < (Calls + 1))
                {
                    var matrix = new Matrix2()
                        {Row0 = new Vector2() {X = 1f, Y = 0f}, Row1 = new Vector2() {X = 10f, Y = 5f}};
                    lib.InvertMatrixByPtr(ref matrix);
                    i++;
                }

                sw.Stop();
                results.Add((double) sw.ElapsedMilliseconds / (double) i);
                Console.WriteLine("[" + j + "]" + " Completed " + --i + " calls in " + sw.ElapsedMilliseconds + "ms");
                Console.WriteLine("[" + j + "]" + " Approximately " +
                                  String.Format("{0:F20}", (double) sw.ElapsedMilliseconds / (double) i)
                                      .TrimEnd('0', '0') +
                                  "ms per call.");
                Console.WriteLine("[" + j + "] Approximately " + Math.Round(Tests / results[j] * 1000) +
                                  " calls per second.");
                results[j] = Math.Round(Tests / results[j] * 1000);
                sw.Reset();
                i = 0;
            }

            Console.WriteLine("Mean Calls Per Second: " + results.Sum() / Tests);

            Console.WriteLine("-- InvertMatrixByValue");
            results.Clear();
            for (var j = 0; j < Tests; j++)
            {
                sw.Start();
                while (i < (Calls + 1))
                {
                    var matrix = new Matrix2()
                        {Row0 = new Vector2() {X = 1f, Y = 0f}, Row1 = new Vector2() {X = 10f, Y = 5f}};
                    lib.InvertMatrixByValue(matrix);
                    i++;
                }

                sw.Stop();
                results.Add((double) sw.ElapsedMilliseconds / (double) i);
                Console.WriteLine("[" + j + "]" + " Completed " + --i + " calls in " + sw.ElapsedMilliseconds + "ms");
                Console.WriteLine("[" + j + "]" + " Approximately " +
                                  String.Format("{0:F20}", (double) sw.ElapsedMilliseconds / (double) i)
                                      .TrimEnd('0', '0') +
                                  "ms per call.");
                Console.WriteLine("[" + j + "] Approximately " + Math.Round(Tests / results[j] * 1000) +
                                  " calls per second.");
                results[j] = Math.Round(Tests / results[j] * 1000);
                sw.Reset();

                i = 0;
            }

            Console.WriteLine("Mean Calls Per Second: " + Math.Round(results.Sum() / Tests));


            Console.WriteLine("-=-= ADL/C =-=-");
            Console.WriteLine();
            sw = new Stopwatch();
            lib = new NativeLibraryBuilder(ImplementationOptions.UseIndirectCalls |
                                           ImplementationOptions.EnableDllMapSupport |
                                           ImplementationOptions.EnableOptimizations |
                                           ImplementationOptions.SuppressSecurity)
                .ActivateInterface<ILazarus>("lz.dll");
            i = 0;
            Console.WriteLine("-- InvertMatrixByPtr");
            results = new List<double>();
            for (var j = 0; j < Tests; j++)
            {
                sw.Start();
                while (i < (Calls + 1))
                {
                    var matrix = new Matrix2()
                        {Row0 = new Vector2() {X = 1f, Y = 0f}, Row1 = new Vector2() {X = 10f, Y = 5f}};
                    lib.InvertMatrixByPtr(ref matrix);
                    i++;
                }

                sw.Stop();
                results.Add((double) sw.ElapsedMilliseconds / (double) i);
                Console.WriteLine("[" + j + "]" + " Completed " + --i + " calls in " + sw.ElapsedMilliseconds + "ms");
                Console.WriteLine("[" + j + "]" + " Approximately " +
                                  String.Format("{0:F20}", (double) sw.ElapsedMilliseconds / (double) i)
                                      .TrimEnd('0', '0') +
                                  "ms per call.");
                Console.WriteLine("[" + j + "] Approximately " + Math.Round(Tests / results[j] * 1000) +
                                  " calls per second.");
                results[j] = Math.Round(Tests / results[j] * 1000);
                sw.Reset();
                i = 0;
            }

            Console.WriteLine("Mean Calls Per Second: " + results.Sum() / Tests);

            Console.WriteLine("-- InvertMatrixByValue");
            results.Clear();
            for (var j = 0; j < Tests; j++)
            {
                sw.Start();
                while (i < (Calls + 1))
                {
                    var matrix = new Matrix2()
                        {Row0 = new Vector2() {X = 1f, Y = 0f}, Row1 = new Vector2() {X = 10f, Y = 5f}};
                    lib.InvertMatrixByValue(matrix);
                    i++;
                }

                sw.Stop();
                results.Add((double) sw.ElapsedMilliseconds / (double) i);
                Console.WriteLine("[" + j + "]" + " Completed " + --i + " calls in " + sw.ElapsedMilliseconds + "ms");
                Console.WriteLine("[" + j + "]" + " Approximately " +
                                  String.Format("{0:F20}", (double) sw.ElapsedMilliseconds / (double) i)
                                      .TrimEnd('0', '0') +
                                  "ms per call.");
                Console.WriteLine("[" + j + "] Approximately " + Math.Round(Tests / results[j] * 1000) +
                                  " calls per second.");
                results[j] = Math.Round(Tests / results[j] * 1000);
                sw.Reset();

                i = 0;
            }

            Console.WriteLine("Mean Calls Per Second: " + Math.Round(results.Sum() / Tests));


#if true

            Console.WriteLine("-=-= RW =-=-");
            Console.WriteLine();
            RW.Lazarus.LoadEntryPoints();
            sw = new Stopwatch();
            i = 0;
            Console.WriteLine("-- InvertMatrixByPtr");
            results.Clear();
            for (var j = 0; j < Tests; j++)
            {
                sw.Start();
                while (i < (Calls + 1))
                {
                    var matrix = new Matrix2()
                        {Row0 = new Vector2() {X = 1f, Y = 0f}, Row1 = new Vector2() {X = 10f, Y = 5f}};
                    RW.Lazarus.InvertMatrixByPtr(ref matrix);
                    i++;
                }

                sw.Stop();
                results.Add((double) sw.ElapsedMilliseconds / (double) i);
                Console.WriteLine("[" + j + "]" + " Completed " + --i + " calls in " + sw.ElapsedMilliseconds + "ms");
                Console.WriteLine("[" + j + "]" + " Approximately " +
                                  String.Format("{0:F20}", (double) sw.ElapsedMilliseconds / (double) i)
                                      .TrimEnd('0', '0') +
                                  "ms per call.");
                Console.WriteLine("[" + j + "] Approximately " + Math.Round(Tests / results[j] * 1000) +
                                  " calls per second.");
                results[j] = Math.Round(Tests / results[j] * 1000);
                sw.Reset();
                i = 0;
            }

            Console.WriteLine("Mean Calls Per Second: " + Math.Round(results.Sum() / Tests));

            Console.WriteLine("-- InvertMatrixByValue");
            results.Clear();
            for (var j = 0; j < Tests; j++)
            {
                sw.Start();
                while (i < (Calls + 1))
                {
                    var matrix = new Matrix2()
                        {Row0 = new Vector2() {X = 1f, Y = 0f}, Row1 = new Vector2() {X = 10f, Y = 5f}};
                    RW.Lazarus.InvertMatrixByValue(matrix);
                    i++;
                }

                sw.Stop();
                results.Add((double) sw.ElapsedMilliseconds / (double) i);
                Console.WriteLine("[" + j + "]" + " Completed " + --i + " calls in " + sw.ElapsedMilliseconds + "ms");
                Console.WriteLine("[" + j + "]" + " Approximately " +
                                  String.Format("{0:F20}", (double) sw.ElapsedMilliseconds / (double) i)
                                      .TrimEnd('0', '0') +
                                  "ms per call.");
                Console.WriteLine("[" + j + "] Approximately " + Math.Round(Tests / results[j] * 1000) +
                                  " calls per second.");
                sw.Reset();
                results[j] = Math.Round(Tests / results[j] * 1000);

                i = 0;
            }

            Console.WriteLine("Mean Calls Per Second: " + Math.Round(results.Sum() / Tests));
#endif
        }
    }
}