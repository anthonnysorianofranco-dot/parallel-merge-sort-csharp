# Benchmark Results

## Environment

- Language: C#
- Framework: .NET 10
- Parallelization: Task Parallel Library (TPL)
- Processor count: 4
- Input size for depth analysis: 1,000,000
- Benchmark runs: 5

## Scalability Results

| Input Size | Average Sequential (ms) | Average Parallel (ms) | Speedup | Efficiency |
|-----------:|------------------------:|----------------------:|--------:|-----------:|
| 100,000    | 31.33                   | 19.33                 | 1.62x   | 40.52%     |
| 500,000    | 169.33                  | 93.00                 | 1.82x   | 45.52%     |
| 1,000,000  | 340.67                  | 189.33                | 1.80x   | 44.98%     |
| 2,000,000  | 686.33                  | 390.00                | 1.76x   | 44.00%     |

## Parallel Depth Analysis

| MaxDepth | Average Sequential (ms) | Average Parallel (ms) | Speedup | Efficiency |
|---------:|------------------------:|----------------------:|--------:|-----------:|
| 1        | 341.40                  | 227.20                | 1.50x   | 37.57%     |
| 2        | 331.20                  | 186.60                | 1.77x   | 44.37%     |
| 3        | 332.80                  | 194.60                | 1.71x   | 42.75%     |

## Analysis

The parallel implementation consistently outperformed the sequential implementation for all tested input sizes.

The scalability benchmark showed a speedup between 1.62x and 1.82x. The highest speedup was obtained with 500,000 elements, reaching 1.82x with an efficiency of 45.52%.

For 1,000,000 elements, the parallel implementation achieved an average execution time of 189.33 ms compared with 340.67 ms for the sequential implementation.

For 2,000,000 elements, the sequential implementation required 686.33 ms while the parallel implementation required 390.00 ms.

The depth analysis showed that MaxDepth 2 provided the best balance between parallelism and task-management overhead.

With MaxDepth 1, the speedup was 1.50x. Increasing the depth to 2 improved the speedup to 1.77x and increased efficiency to 44.37%.

Increasing MaxDepth from 2 to 3 did not improve performance. The speedup decreased from 1.77x to 1.71x, indicating that the additional parallel tasks introduced overhead that outweighed the potential benefit.

Therefore, MaxDepth 2 was selected as the recommended configuration for the four-core processor used during testing.

The results demonstrate that parallel Merge Sort can take advantage of multiple processor cores while maintaining a significant performance improvement. However, speedup does not reach the theoretical maximum because of task-management, synchronization, memory access and merge overhead.