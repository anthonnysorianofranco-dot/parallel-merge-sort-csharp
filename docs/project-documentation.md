
### `docs/project-documentation.md`

`markdown`
# Project Documentation

## 1. Project Overview

Parallel Merge Sort is a final project for the Parallel Programming course.

The project demonstrates how a traditional sequential algorithm can be adapted to use parallel execution.

The selected algorithm is Merge Sort, using recursive decomposition as the parallelization strategy.

The project compares sequential and parallel execution using real performance measurements.

## 2. Project Objectives

The main objective is to implement and evaluate a parallel Merge Sort algorithm.

Specific objectives:

- Implement sequential Merge Sort.
- Implement parallel Merge Sort.
- Use recursive decomposition.
- Use the Task Parallel Library (TPL).
- Execute independent tasks concurrently.
- Control the maximum parallel recursion depth.
- Measure execution time.
- Calculate Speedup.
- Calculate Efficiency.
- Evaluate scalability.
- Validate the correctness of the implementations.
- Document the results.

## 3. Technologies

The project uses the following technologies:

| Technology | Purpose |
|---|---|
| C# | Programming language |
| .NET 10 | Application framework |
| Task Parallel Library | Parallel execution |
| xUnit | Automated testing |
| Git | Version control |
| GitHub | Source code repository |
| Visual Studio | Development environment |

## 4. Project Structure

The repository is organized as follows:

`text`
parallel-merge-sort-csharp/
|
+-- docs/
|   +-- architecture.md
|   +-- technical-design.md
|   +-- project-documentation.md
|
+-- metrics/
|   +-- benchmark-results.md
|
+-- src/
|   +-- ParallelMergeSort.App/
|       +-- Program.cs
|       +-- MergeSorter.cs
|       +-- ParallelMergeSorter.cs
|       +-- ParallelMergeSort.App.csproj
|
+-- tests/
|   +-- ParallelMergeSort.Tests/
|       +-- ParallelMergeSort.Tests.csproj
|       +-- UnitTest1.cs
|
+-- README.md
+-- LICENSE
+-- .gitignore

---

## 5. Sequential Implementation

The sequential implementation is contained in MergeSorter.cs.

The algorithm follows the traditional Merge Sort process:

Divide the array into two halves.
Recursively sort the left half.
Recursively sort the right half.
Merge the two sorted halves.

The algorithm has a time complexity of:

O(n log n)

The sequential implementation serves as the baseline for evaluating the parallel version.

---

## 6. Parallel Implementation

The parallel implementation is contained in ParallelMergeSorter.cs.

It uses recursive decomposition and the Task Parallel Library.

When the configured maximum depth has not been reached, the algorithm creates two tasks:

Task 1 -> Sort left half
Task 2 -> Sort right half

The application waits for both tasks:

Task.WaitAll(leftTask, rightTask);

After both branches finish, the algorithm performs the merge operation.

When the maximum parallel depth is reached, the remaining work is executed sequentially.

---

## 7. Performance Evaluation

The project measures the performance of both implementations.

The main metrics are:

Execution Time

The time required to complete the sorting operation.

Speedup
Speedup = Sequential Time / Parallel Time
Efficiency
Efficiency = Speedup / Processor Count × 100

The current test environment has four logical processors.

---

## 8. Benchmark Results

The scalability benchmark tested four input sizes.

| Input Size | Sequential Average | Parallel Average | Speedup | Efficiency |
| ---------: | -----------------: | ---------------: | ------: | ---------: |
|    100,000 |           31.33 ms |         19.33 ms |   1.62x |     40.52% |
|    500,000 |          169.33 ms |         93.00 ms |   1.82x |     45.52% |
|  1,000,000 |          340.67 ms |        189.33 ms |   1.80x |     44.98% |
|  2,000,000 |          686.33 ms |        390.00 ms |   1.76x |     44.00% |

The parallel implementation was faster for every tested input size.

The best speedup in this benchmark was 1.82x with 500,000 elements.

---

## 9. Parallelization Depth Results

The project also evaluates different maximum parallel depths.

Recent benchmark results for 1,000,000 elements were:

| MaxDepth | Average Sequential | Average Parallel | Speedup | Efficiency |
| -------: | -----------------: | ---------------: | ------: | ---------: |
|        1 |          358.80 ms |        236.60 ms |   1.52x |     37.91% |
|        2 |          388.20 ms |        197.00 ms |   1.97x |     49.26% |
|        3 |          358.20 ms |        205.60 ms |   1.74x |     43.56% |

The best configuration in this benchmark was MaxDepth 2, with a speedup of 1.97x and an efficiency of 49.26%.

The results demonstrate that increasing the amount of parallelism does not necessarily produce better performance.

A higher depth can introduce additional task-management and synchronization overhead.

---

## 10. Automated Testing

The project uses xUnit for automated testing.

The test suite currently reports:

Total tests: 10
Passed: 10
Failed: 0
Skipped: 0

The tests verify that the sorting implementations correctly handle different input conditions.

Correctness validation is important because performance improvements are only useful if the final sorted result remains correct.

---

## 11. Scalability

Scalability was evaluated by increasing the number of elements processed.

The tested sizes were:

100,000
500,000
1,000,000
2,000,000

The results show that the parallel implementation maintains a significant performance advantage as the input size increases.

For larger inputs, there is more computational work available for the parallel tasks, allowing the benefits of concurrent execution to become more visible.

However, speedup is limited by task scheduling, synchronization, memory access, and merge overhead.

---

## 12. Correctness Validation

After each benchmark execution, the application verifies that the resulting arrays are sorted.

The validation checks every pair of adjacent elements:

array[i - 1] <= array[i]

If any element violates this condition, the result is considered incorrect.

The benchmark executions completed with both sequential and parallel sorting results correctly validated.

---

## 13. Conclusions

The project demonstrates that recursive decomposition can be effectively applied to Merge Sort.

The parallel implementation achieved lower execution times than the sequential implementation across the tested input sizes.

The results also demonstrate that:

Parallel execution can improve performance.
Larger datasets provide more opportunity for parallelism.
The number of tasks must be controlled.
Maximum recursion depth affects performance.
Synchronization introduces overhead.
Parallel execution does not guarantee linear speedup.
Correctness must be validated alongside performance.

The current implementation successfully demonstrates the main concepts required for a parallel programming project: concurrent execution, shared data, synchronization, recursive decomposition, performance measurement, and scalability.