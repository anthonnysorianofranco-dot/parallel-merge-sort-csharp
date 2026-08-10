using System.Diagnostics;

namespace ParallelMergeSort.App;

class Program
{
    static void Main()
    {
        const int size = 1_000_000;

        Random random = new();
        int[] data = new int[size];

        for (int i = 0; i < size; i++)
            data[i] = random.Next();

        Console.WriteLine($"Sorting {size:N0} numbers...");

        Stopwatch sw = Stopwatch.StartNew();

        MergeSorter.MergeSort(data);

        sw.Stop();

        Console.WriteLine($"Sequential time: {sw.ElapsedMilliseconds} ms");
    }
}