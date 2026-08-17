namespace ParallelMergeSort.App;

public static class ParallelMergeSorter
{
    public static void Sort(int[] array, int maxDepth = 2)
    {
        if (array.Length <= 1)
            return;

        int[] temp = new int[array.Length];

        SortRecursive(array, temp, 0, array.Length - 1, 0, maxDepth);
    }

    private static void SortRecursive(
        int[] array,
        int[] temp,
        int left,
        int right,
        int depth,
        int maxDepth)
    {
        if (left >= right)
            return;

        int middle = left + (right - left) / 2;

        if (depth < maxDepth)
        {
            Task leftTask = Task.Run(() =>
                SortRecursive(
                    array,
                    temp,
                    left,
                    middle,
                    depth + 1,
                    maxDepth));

            Task rightTask = Task.Run(() =>
                SortRecursive(
                    array,
                    temp,
                    middle + 1,
                    right,
                    depth + 1,
                    maxDepth));

            Task.WaitAll(leftTask, rightTask);
        }
        else
        {
            SortRecursive(
                array,
                temp,
                left,
                middle,
                depth + 1,
                maxDepth);

            SortRecursive(
                array,
                temp,
                middle + 1,
                right,
                depth + 1,
                maxDepth);
        }

        MergeSorter.Merge(
            array,
            temp,
            left,
            middle,
            right);
    }
}