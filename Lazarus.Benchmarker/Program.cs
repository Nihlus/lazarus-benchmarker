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
            Console.WriteLine("Lazarus Benchmarker");
            Console.WriteLine();
            Console.WriteLine("-=-= ADL =-=-");
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
                Console.WriteLine("["+j+"]"+" Completed " + --i + " calls in " + sw.ElapsedMilliseconds + "ms");
                Console.WriteLine("["+j+"]"+" Approximately " +
                                  String.Format("{0:F20}", (double) sw.ElapsedMilliseconds / (double) i).TrimEnd('0','0') +
                                  "ms per call.");
                Console.WriteLine("["+j+"] Approximately "+Math.Round(Tests/results[j]*1000)+" calls per second.");
                sw.Reset();
                i = 0;
            }
            Console.WriteLine("Mean: "+String.Format("{0:F20}",results.Sum() / Tests).TrimEnd('0','0')+"ms");

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
                Console.WriteLine("["+j+"]"+" Completed " + --i + " calls in " + sw.ElapsedMilliseconds + "ms");
                Console.WriteLine("["+j+"]"+" Approximately " +
                                  String.Format("{0:F20}", (double) sw.ElapsedMilliseconds / (double) i).TrimEnd('0','0') +
                                  "ms per call.");
                Console.WriteLine("["+j+"] Approximately "+Math.Round(Tests/results[j]*1000)+" calls per second.");
                sw.Reset();

                i = 0;
            }
            Console.WriteLine("Mean: "+String.Format("{0:F20}",results.Sum() / Tests).TrimEnd('0','0')+"ms");
            
            
            
            
            
            
            
            
            
            #if NET471
            
            Console.WriteLine("-=-= RW =-=-");
            Console.WriteLine();
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
                Console.WriteLine("["+j+"]"+" Completed " + --i + " calls in " + sw.ElapsedMilliseconds + "ms");
                Console.WriteLine("["+j+"]"+" Approximately " +
                                  String.Format("{0:F20}", (double) sw.ElapsedMilliseconds / (double) i).TrimEnd('0','0') +
                                  "ms per call.");
                Console.WriteLine("["+j+"] Approximately "+Math.Round(Tests/results[j]*1000)+" calls per second.");
                sw.Reset();
                i = 0;
            }
            Console.WriteLine("Mean: "+String.Format("{0:F20}",results.Sum() / Tests).TrimEnd('0','0')+"ms");

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
                Console.WriteLine("["+j+"]"+" Completed " + --i + " calls in " + sw.ElapsedMilliseconds + "ms");
                Console.WriteLine("["+j+"]"+" Approximately " +
                                  String.Format("{0:F20}", (double) sw.ElapsedMilliseconds / (double) i).TrimEnd('0','0') +
                                  "ms per call.");
                Console.WriteLine("["+j+"] Approximately "+Math.Round(Tests/results[j]*1000)+" calls per second.");
                sw.Reset();

                i = 0;
            }
            Console.WriteLine("Mean: "+String.Format("{0:F20}",results.Sum() / Tests).TrimEnd('0','0')+"ms");
    #endif
        }
    }
}