``` ini

BenchmarkDotNet=v0.11.2, OS=Windows 10.0.17134.345 (1803/April2018Update/Redstone4)
AMD FX-8370, 1 CPU, 8 logical and 4 physical cores
Frequency=14318180 Hz, Resolution=69.8413 ns, Timer=HPET
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT
  Job-NXYTOA : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|              Method |         Method |      Mean |     Error |   StdDev |    Median |        P95 | Rank |
|-------------------- |--------------- |----------:|----------:|---------:|----------:|-----------:|-----:|
|   **InvertMatrixByPtr** |     **ADL, calli** |  **89.29 ns** |  **49.95 ns** | **145.7 ns** |  **73.33 ns** |   **562.2 ns** |    **1** |
| InvertMatrixByValue |     ADL, calli | 443.00 ns |  95.23 ns | 277.8 ns | 537.78 ns | 1,026.7 ns |    3 |
|   **InvertMatrixByPtr** | **ADL, delegates** | **477.31 ns** | **117.22 ns** | **336.3 ns** | **513.33 ns** | **1,002.2 ns** |    **4** |
| InvertMatrixByValue | ADL, delegates | 683.16 ns | 111.06 ns | 318.7 ns | 611.11 ns | 1,100.0 ns |    5 |
|   **InvertMatrixByPtr** |      **Rewritten** | **263.90 ns** |  **99.94 ns** | **291.5 ns** |  **24.44 ns** |   **586.7 ns** |    **2** |
| InvertMatrixByValue |      Rewritten | 376.33 ns | 107.79 ns | 312.7 ns | 456.30 ns |   945.2 ns |    2 |
