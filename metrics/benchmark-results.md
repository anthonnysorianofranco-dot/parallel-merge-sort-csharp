# Benchmark Results

## Environment

- Language: C#
- Framework: .NET 10
- Parallelization: Task Parallel Library (TPL)
- Processor count: 4
- Parallel max depth: 2
- Benchmark runs per input size: 3

## Scalability Results

| Input Size | Average Sequential (ms) | Average Parallel (ms) | Speedup | Efficiency |
|-----------:|------------------------:|----------------------:|--------:|-----------:|
| 100,000    | 31.33                   | 19.33                 | 1.62x   | 40.52%     |
| 500,000    | 169.33                  | 93.00                 | 1.82x   | 45.52%     |
| 1,000,000  | 340.67                  | 189.33                | 1.80x   | 44.98%     |
| 2,000,000  | 686.33                  | 390.00                 | 1.76x   | 44.00%     |

## Analysis

The parallel implementation consistently outperformed the sequential implementation for all tested input sizes.

The highest speedup was achieved with 500,000 elements, reaching 1.82x with an efficiency of 45.52%.

For 1,000,000 elements, the parallel implementation achieved an average execution time of 189.33 ms compared with 340.67 ms for the sequential implementation.

For 2,000,000 elements, the sequential implementation required 686.33 ms while the parallel implementation required 390.00 ms.

The results demonstrate that the parallel Merge Sort can take advantage of the available four processor cores and maintain a significant performance improvement as the input size increases.

The results also show that increasing the problem size provides enough computational work to benefit from parallel execution, although speedup does not increase indefinitely due to synchronization, task-management and merge overhead.