# Parallel Merge Sort in C#

## Final Project – Parallel Programming

**Topic:** Recursive Decomposition
**Algorithm:** Parallel Merge Sort
**Language:** C# (.NET 10)
**Technology:** Task Parallel Library (TPL)

## Project objective

Implement and evaluate a recursive parallel Merge Sort algorithm capable of sorting large volumes of data while comparing sequential and parallel execution in terms of:

* Execution time
* Speedup
* Efficiency
* Scalability
* CPU utilization

## Repository structure

* `/docs` → project documentation
* `/src` → source code
* `/tests` → unit tests
* `/metrics` → performance results and charts

## Team

* Anthonny Brayhan Soriano Franco (Leader)

## Status

* [x] Topic approved
* [x] Sequential implementation
* [x] Parallel implementation
* [x] Performance benchmarks
* [x] Final documentation

## Performance Summary

The parallel implementation was evaluated using a 4-core processor.

The best configuration was `MaxDepth = 2`, achieving:

- Average parallel execution time: **186.60 ms**
- Average sequential execution time: **331.20 ms**
- Speedup: **1.77x**
- Efficiency: **44.37%**

The results showed that increasing the recursion depth beyond 2 did not improve performance due to additional task-management overhead.

The scalability benchmark also showed consistent performance improvements for input sizes from 100,000 to 2,000,000 elements.