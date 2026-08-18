# System Architecture

## 1. Architecture Overview

Parallel Merge Sort is a C# application designed to compare a sequential implementation of Merge Sort with a parallel implementation using the Task Parallel Library (TPL).

The parallel implementation uses recursive decomposition to divide the sorting problem into smaller independent subproblems that can be executed concurrently.

The architecture separates the application into sorting logic, execution and benchmarking, automated testing, documentation, and performance metrics.

---

## 2. System Components

The project contains the following main components.

### `Program.cs`

Acts as the entry point of the application.

Responsibilities:

- Generate test data.
- Execute sequential Merge Sort.
- Execute parallel Merge Sort.
- Measure execution times.
- Calculate Speedup.
- Calculate Efficiency.
- Execute performance benchmarks.
- Compare different parallelization depths.
- Validate sorting results.

### `MergeSorter.cs`

Contains the sequential Merge Sort implementation.

Responsibilities:

- Recursively divide the array.
- Sort each portion sequentially.
- Merge sorted portions.
- Provide the `Merge` operation used by the sorting implementations.

### `ParallelMergeSorter.cs`

Contains the parallel Merge Sort implementation.

Responsibilities:

- Apply recursive decomposition.
- Create concurrent tasks using `Task.Run`.
- Execute left and right branches concurrently.
- Control the maximum parallel recursion depth.
- Synchronize tasks using `Task.WaitAll`.
- Use sequential execution after reaching the configured depth.

### `ParallelMergeSort.Tests`

Contains automated tests developed with xUnit.

The tests verify that the sorting implementations correctly process different types of input.

### `metrics/`

Contains performance measurements and benchmark results.

The metrics include:

- Sequential execution time.
- Parallel execution time.
- Speedup.
- Efficiency.
- Scalability results.
- Parallelization depth comparisons.

---

## 3. Execution Flow

The general execution flow is:

`text`
                    Start
                      |
                      v
                Generate data
                      |
             +--------+--------+
             |                 |
             v                 v
      Sequential Sort    Parallel Sort
             |                 |
             |          Recursive division
             |                 |
             |          +------+------+
             |          |             |
             |          v             v
             |      Left Task     Right Task
             |          |             |
             |          +------+------+
             |                 |
             |            Task.WaitAll
             |                 |
             |               Merge
             |                 |
             +--------+--------+
                      |
                      v
              Validate results
                      |
                      v
              Calculate metrics
                      |
                      v
                     End

The application generates the same input data for both implementations.

The sequential algorithm sorts one copy of the data, while the parallel algorithm sorts another copy.

The execution times are measured independently and then used to calculate performance metrics.

---

## 4. Parallel Decomposition

The parallel algorithm uses recursive decomposition.

The original array is divided into two smaller subproblems.

                    Array
                   /     \
                  /       \
          Left half       Right half
             /               \
            /                 \
       Left task          Right task

Each branch can be processed independently because the two portions of the array do not overlap.

The parallel implementation creates a task for each branch.

                    Array
                   /     \
                  /       \
            Task 1       Task 2
              |             |
        Left recursion  Right recursion
              |             |
              +------+------+
                     |
                   Merge

The recursion continues until the configured maximum parallel depth is reached.

After reaching this limit, the remaining recursive operations are executed sequentially.

This prevents the application from creating an excessive number of tasks.

---

## 5. Concurrency Model

The application uses the Task Parallel Library (TPL).

The main concurrent operations are created using Task.Run.

Conceptually, the parallel algorithm performs:

Task.Run(Left branch)
Task.Run(Right branch)

        |
        v

Task.WaitAll()

        |
        v

      Merge

The left and right branches can execute concurrently on different processor cores.

The implementation uses a maximum depth to control the amount of concurrency.

The current test environment has four logical processors.

---

## 6. Synchronization

Synchronization is required before the two sorted portions can be merged.

The implementation uses:

Task.WaitAll(leftTask, rightTask);

This guarantees that both recursive tasks have completed before the `Merge` operation begins.

The synchronization sequence is:

Left Task  --------\
                    >---- Task.WaitAll ----> Merge
Right Task --------/

Without this synchronization, the merge operation could execute before one of the portions had finished sorting.

---

## 7. Data Flow

The application uses arrays of integers as the main data structure.

The data flow is:

Random data
    |
    +--------------------+
    |                    |
    v                    v
Sequential copy    Parallel copy
    |                    |
    v                    v
Merge Sort          Parallel Merge Sort
    |                    |
    v                    v
Validation          Validation
    |                    |
    +---------+----------+
              |
              v
       Performance metrics

Both algorithms receive equivalent input data so that the performance comparison is meaningful.

---

## 8. Scalability

The application evaluates scalability by testing different input sizes.

The benchmark includes:

- 100,000 elements.
- 500,000 elements.
- 1,000,000 elements.
- 2,000,000 elements.

The results demonstrate that the parallel implementation maintains a performance advantage as the input size increases.

For larger datasets, there is enough computational work to compensate for part of the overhead introduced by task management and synchronization.

---

## 9. Performance Metrics

The architecture supports the calculation of several performance metrics.

Execution Time

Measures how long each implementation takes to sort the input.

Speedup
Speedup = Sequential Time / Parallel Time

A value greater than 1 means that the parallel implementation is faster.

Efficiency
Efficiency = Speedup / Number of Processors × 100

This indicates how effectively the available processor resources are being used.

---

## 10. Architecture Summary

The system follows an architecture centered around the Merge Sort algorithm.

The sequential and parallel implementations share the same merge operation while using different execution strategies.

The parallel implementation uses recursive decomposition, Task, and Task.WaitAll to execute independent branches concurrently.

The architecture also includes automated tests and performance benchmarks to verify correctness and evaluate the benefits of parallel execution.