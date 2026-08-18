using System.Diagnostics;

namespace ParallelMergeSort.App;

class Program
{
    static void Main()
    {
        const int inputSize = 1_000_000;
        const int runs = 5;

        int[] depths = { 1, 2, 3 };

        Console.WriteLine("Parallel Merge Sort - Performance Analysis");
        Console.WriteLine();
        Console.WriteLine($"Processor count: {Environment.ProcessorCount}");
        Console.WriteLine($"Input size: {inputSize:N0}");
        Console.WriteLine($"Benchmark runs per depth: {runs}");
        Console.WriteLine();

        foreach (int depth in depths)
        {
            RunDepthBenchmark(
                inputSize,
                depth,
                runs);
        }
    }

    static void RunDepthBenchmark(
        int size,
        int maxDepth,
        int runs)
    {
        Console.WriteLine(
            $"===== MaxDepth: {maxDepth} =====");

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

            long sequentialTime =
                sequentialTimer.ElapsedMilliseconds;

            long parallelTime =
                parallelTimer.ElapsedMilliseconds;

            sequentialTotal += sequentialTime;
            parallelTotal += parallelTime;

            Console.WriteLine(
                $"Run {run}: " +
                $"Sequential = {sequentialTime} ms | " +
                $"Parallel = {parallelTime} ms");
        }

        // ==========================
        // Metrics
        // ==========================

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
            {
                return false;
            }
        }

        return true;
    }
}