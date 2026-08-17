using ParallelMergeSort.App;

namespace ParallelMergeSort.Tests;

public class MergeSortTests
{
    [Fact]
    public void SequentialSort_ShouldSortRandomArray()
    {
        int[] array =
        {
            9, 4, 7, 1, 3, 8, 2, 6, 5
        };

        MergeSorter.MergeSort(array);

        Assert.True(IsSorted(array));
    }

    [Fact]
    public void ParallelSort_ShouldSortRandomArray()
    {
        int[] array =
        {
            9, 4, 7, 1, 3, 8, 2, 6, 5
        };

        ParallelMergeSorter.Sort(array, 2);

        Assert.True(IsSorted(array));
    }

    [Fact]
    public void SequentialSort_ShouldHandleEmptyArray()
    {
        int[] array = Array.Empty<int>();

        MergeSorter.MergeSort(array);

        Assert.Empty(array);
    }

    [Fact]
    public void ParallelSort_ShouldHandleEmptyArray()
    {
        int[] array = Array.Empty<int>();

        ParallelMergeSorter.Sort(array, 2);

        Assert.Empty(array);
    }

    [Fact]
    public void ParallelSort_ShouldHandleSingleElement()
    {
        int[] array = { 42 };

        ParallelMergeSorter.Sort(array, 2);

        Assert.Equal(42, array[0]);
    }

    [Fact]
    public void ParallelSort_ShouldHandleAlreadySortedArray()
    {
        int[] array =
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9
        };

        ParallelMergeSorter.Sort(array, 2);

        Assert.True(IsSorted(array));
    }

    [Fact]
    public void ParallelSort_ShouldHandleReverseSortedArray()
    {
        int[] array =
        {
            9, 8, 7, 6, 5, 4, 3, 2, 1
        };

        ParallelMergeSorter.Sort(array, 2);

        Assert.True(IsSorted(array));
    }

    [Fact]
    public void ParallelSort_ShouldHandleDuplicateValues()
    {
        int[] array =
        {
            5, 2, 5, 1, 2, 8, 5, 1
        };

        ParallelMergeSorter.Sort(array, 2);

        Assert.True(IsSorted(array));
    }

    [Fact]
    public void ParallelSort_ShouldProduceSameResultAsSequential()
    {
        int[] sequentialArray =
        {
            10, 3, 7, 2, 8, 1, 9, 4, 6, 5
        };

        int[] parallelArray =
            (int[])sequentialArray.Clone();

        MergeSorter.MergeSort(sequentialArray);

        ParallelMergeSorter.Sort(
            parallelArray,
            2);

        Assert.Equal(
            sequentialArray,
            parallelArray);
    }

    [Fact]
    public void ParallelSort_ShouldSortLargeArray()
    {
        Random random = new(12345);

        int[] array = new int[100_000];

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = random.Next();
        }

        ParallelMergeSorter.Sort(array, 2);

        Assert.True(IsSorted(array));
    }

    private static bool IsSorted(int[] array)
    {
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i - 1] > array[i])
                return false;
        }

        return true;
    }
}