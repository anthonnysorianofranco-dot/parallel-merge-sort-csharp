using System.Diagnostics;

namespace ParallelMergeSort.App;

class Program
{
    static void Main()
    {
        int[] inputSizes =
        {
            100_000,
            500_000,
            1_000_000,
            2_000_000
        };

        const int maxDepth = 2;
        const int runs = 3;

        Console.WriteLine("Parallel Merge Sort - Scalability Benchmark");
        Console.WriteLine();
        Console.WriteLine($"Processor count: {Environment.ProcessorCount}");
        Console.WriteLine($"MaxDepth: {maxDepth}");
        Console.WriteLine($"Runs per input size: {runs}");
        Console.WriteLine();

        foreach (int size in inputSizes)
        {
            RunScalabilityBenchmark(
                size,
                maxDepth,
                runs);
        }
    }

    static void RunScalabilityBenchmark(
        int size,
        int maxDepth,
        int runs)
    {
        Console.WriteLine(
            $"===== Input Size: {size:N0} =====");

        long sequentialTotal = 0;
        long parallelTotal = 0;

        for (int run = 1; run <= runs; run++)
        {
            Random random = new(12345);

            int[] originalData = new int[size];

            for (int i = 0; i < size; i++)
            {
                originalData[i] = random.Next();
            }

            // ==========================
            // Sequential
            // ==========================

            int[] sequentialData =
                (int[])originalData.Clone();

            Stopwatch sequentialTimer =
                Stopwatch.StartNew();

            MergeSorter.MergeSort(sequentialData);

            sequentialTimer.Stop();

            long sequentialTime =
                sequentialTimer.ElapsedMilliseconds;

            // ==========================
            // Parallel
            // ==========================

            int[] parallelData =
                (int[])originalData.Clone();

            Stopwatch parallelTimer =
                Stopwatch.StartNew();

            ParallelMergeSorter.Sort(
                parallelData,
                maxDepth);

            parallelTimer.Stop();

            long parallelTime =
                parallelTimer.ElapsedMilliseconds;

            // ==========================
            // Validation
            // ==========================

            bool sequentialCorrect =
                IsSorted(sequentialData);

            bool parallelCorrect =
                IsSorted(parallelData);

            if (!sequentialCorrect || !parallelCorrect)
            {
                Console.WriteLine(
                    $"Run {run}: ERROR - incorrect sorting.");

                return;
            }

            sequentialTotal += sequentialTime;
            parallelTotal += parallelTime;

            Console.WriteLine(
                $"Run {run}: " +
                $"Sequential = {sequentialTime} ms | " +
                $"Parallel = {parallelTime} ms");
        }

        double sequentialAverage =
            (double)sequentialTotal / runs;

        double parallelAverage =
            (double)parallelTotal / runs;

        double speedup =
            sequentialAverage / parallelAverage;

        double efficiency =
            speedup / Environment.ProcessorCount * 100;

        Console.WriteLine();

        Console.WriteLine(
            $"Average Sequential: {sequentialAverage:F2} ms");

        Console.WriteLine(
            $"Average Parallel:   {parallelAverage:F2} ms");

        Console.WriteLine(
            $"Speedup:             {speedup:F2}x");

        Console.WriteLine(
            $"Efficiency:          {efficiency:F2}%");

        Console.WriteLine();
    }

    static bool IsSorted(int[] array)
    {
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i - 1] > array[i])
                return false;
        }

        return true;
    }
}