
### `docs/technical-design.md`

`markdown`
# Technical Design

## 1. General Description

Parallel Merge Sort is an application developed in C# that compares a sequential implementation of Merge Sort with a parallel implementation using the Task Parallel Library (TPL).

The parallel implementation applies recursive decomposition to divide the sorting problem into smaller independent subproblems.

The main objective is to evaluate the advantages and limitations of parallel execution using:

- Execution time.
- Speedup.
- Efficiency.
- Scalability.
- Different levels of parallelism.

## 2. System Architecture

The project is organized into several components.

### `Program.cs`

The entry point of the application.

Responsibilities:

- Generate input data.
- Execute the sequential algorithm.
- Execute the parallel algorithm.
- Measure execution time.
- Calculate Speedup and Efficiency.
- Run performance benchmarks.
- Validate results.

### `MergeSorter.cs`

Contains the sequential Merge Sort implementation.

The class performs recursive division of the array and uses the `Merge` operation to combine sorted portions.

### `ParallelMergeSorter.cs`

Contains the parallel implementation.

Main characteristics:

- Recursive decomposition.
- `Task.Run`.
- Concurrent execution of left and right branches.
- Configurable maximum depth.
- `Task.WaitAll` synchronization.

### `ParallelMergeSort.Tests`

Contains automated xUnit tests used to verify the correctness of the sorting algorithms.

### `metrics/`

Contains benchmark results and performance information.

## 3. Recursive Decomposition

Merge Sort follows the divide-and-conquer strategy.

The algorithm repeatedly divides the array into two halves.

`text`
                 Array
                /     \
               /       \
          Left half   Right half
             |             |
          Divide          Divide
             |             |
          Smaller       Smaller
          problems      problems

After the recursive sorting operations finish, the two sorted portions are merged.

The parallel version takes advantage of the fact that the left and right portions can be processed independently.

---

## 4. Parallelization Strategy

The parallel implementation uses Task to execute the two recursive branches concurrently.

The main strategy is:

                Array
               /     \
              /       \
       Left branch   Right branch
           |             |
        Task.Run      Task.Run
           |             |
           +------+------+
                  |
             Task.WaitAll
                  |
                Merge

The implementation does not create tasks at every recursion level.

Instead, it uses a configurable maxDepth.

When the current depth is below the maximum depth, the left and right branches are executed using separate tasks.

When the maximum depth is reached, the remaining recursive operations execute sequentially.

This approach limits task creation overhead.

---

## 5. Maximum Parallel Depth

The maximum parallel depth controls how much concurrency is introduced into the algorithm.

The application uses a maximum depth of 2 for the main scalability benchmark because the test machine has four logical processors.

The relationship can be represented as:

Depth 0
        |
        +----------------+
        |                |
     Depth 1          Depth 1
      /   \            /   \
     /     \          /     \
   Depth 2 Depth 2  Depth 2 Depth 2

This creates four primary recursive portions.

Using excessive depth could create more tasks than the processor can efficiently execute, increasing task scheduling and synchronization overhead.

---

## 6. Synchronization

The parallel implementation must ensure that both recursive branches have completed before merging their results.

This is achieved using:

Task.WaitAll(leftTask, rightTask);

The synchronization sequence is:

Left Task  --------\
                    \
                     >---- WaitAll ----> Merge
                    /
Right Task --------/

Task.WaitAll prevents the merge operation from starting before both branches are finished.

---

## 7. Merge Operation

The Merge operation combines two already sorted portions of the array.

For example:

Left:  [1, 4, 7]
Right: [2, 3, 8]


Result:


[1, 2, 3, 4, 7, 8]

The merge operation uses a temporary array to store intermediate results before copying them back to the original array.

Both sequential and parallel implementations use the same merge logic.

---

## 8. Complexity Analysis

Merge Sort has a time complexity of:

O(n log n)

for the best, average, and worst cases.

The additional temporary storage required by the algorithm is:

O(n)

The parallel version maintains the same general computational complexity, but distributes independent recursive work among processor resources.

The practical performance improvement depends on:

Number of processor cores.
Input size.
Task scheduling overhead.
Synchronization overhead.
Merge operations.
Maximum parallel depth.

---

## 9. Performance Metrics

The application calculates three main performance metrics.

Execution Time

The elapsed time required to sort the input.

Speedup
Speedup = Sequential Time / Parallel Time

For example, if the sequential implementation takes 400 ms and the parallel implementation takes 200 ms:

Speedup = 400 / 200
Speedup = 2.0x
Efficiency
Efficiency = Speedup / Processor Count × 100

With four processors and a speedup of 2.0x:

Efficiency = 2.0 / 4 × 100
Efficiency = 50%

---

## 10. Benchmark Design

The benchmark executes multiple runs for each configuration.

For scalability testing, the application uses:

100,000
500,000
1,000,000
2,000,000

elements.

For parallelization-depth testing, the application compares:

MaxDepth = 1
MaxDepth = 2
MaxDepth = 3

Multiple runs are performed to reduce the effect of individual execution variations.

Average execution time is used for the main performance comparison.

---

## 11. Testing Strategy

Automated tests are implemented using xUnit.

The test suite verifies the correctness of the sorting implementations using different types of input.

The tests include scenarios such as:

Empty arrays.
Single-element arrays.
Already sorted arrays.
Reverse-ordered arrays.
Arrays with duplicate values.
Random values.

The test suite currently reports:

Total tests: 10
Passed: 10
Failed: 0
Skipped: 0

This confirms that the implemented sorting operations produce correctly ordered results for the tested cases.

---

## 12. Scalability

The parallel implementation was evaluated using increasing input sizes.

The benchmark produced the following averages:

| Input Size | Sequential |  Parallel | Speedup | Efficiency |
| ---------: | ---------: | --------: | ------: | ---------: |
|    100,000 |   31.33 ms |  19.33 ms |   1.62x |     40.52% |
|    500,000 |  169.33 ms |  93.00 ms |   1.82x |     45.52% |
|  1,000,000 |  340.67 ms | 189.33 ms |   1.80x |     44.98% |
|  2,000,000 |  686.33 ms | 390.00 ms |   1.76x |     44.00% |


The parallel implementation was faster for every tested input size.

The results also show that speedup does not increase indefinitely because parallel execution introduces task-management, synchronization, and merge overhead.

---

## 13. Limitations

The implementation has several limitations.

Processor dependency

Performance depends on the number of available processor cores.

Task overhead

Creating and scheduling tasks introduces additional overhead.

Synchronization overhead

The recursive branches must synchronize before the merge operation.

Memory usage

Merge Sort requires additional memory for temporary arrays.

Benchmark variability

Execution times can vary between runs because of operating system activity, background processes, processor scheduling, and other environmental factors.

---

## 14. Design Conclusion

The technical design demonstrates how recursive decomposition can be applied to Merge Sort to introduce parallel execution.

The implementation uses TPL tasks, controlled recursion depth, and synchronization to execute independent portions of the algorithm concurrently.

The benchmark and automated tests provide evidence of both correctness and performance improvement.