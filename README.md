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
* [ ] Final documentation

## Performance Results

The parallel implementation was evaluated using 4 processor cores and a maximum parallel recursion depth of 2.

| Input Size | Sequential (ms) | Parallel (ms) | Speedup | Efficiency |
|-----------:|----------------:|--------------:|--------:|-----------:|
| 100,000    | 31.33           | 19.33         | 1.62x   | 40.52%     |
| 500,000    | 169.33          | 93.00         | 1.82x   | 45.52%     |
| 1,000,000  | 340.67          | 189.33        | 1.80x   | 44.98%     |
| 2,000,000  | 686.33          | 390.00        | 1.76x   | 44.00%     |

The results show that the parallel implementation consistently outperformed the sequential implementation. The highest observed speedup was 1.82x for 500,000 elements.

Automated unit tests: **10/10 passed**.