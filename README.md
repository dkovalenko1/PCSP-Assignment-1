# Parallel Matrix Row-Maximum Benchmark

This C# project fills a square matrix with random values and places the maximum element of each row on the main diagonal. Sequential and multithreaded implementations are benchmarked across different matrix sizes and thread counts, with results exported to CSV for performance and speedup charts.

## Run

Requires [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).

```bash
dotnet run --project Parallel-Matrix-Benchmark -c Release
```

Benchmark results are written to `benchmark_results.csv`. Visualization scripts and generated charts are available in `Parallel-Matrix-Benchmark/Visualizations`.