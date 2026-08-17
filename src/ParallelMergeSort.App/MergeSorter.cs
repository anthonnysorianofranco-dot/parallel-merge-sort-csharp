namespace ParallelMergeSort.App;

public static class MergeSorter
{
    public static void MergeSort(int[] array)
    {
        if (array.Length <= 1)
            return;

        int[] temp = new int[array.Length];

        MergeSortRecursive(
            array,
            temp,
            0,
            array.Length - 1);
    }

    private static void MergeSortRecursive(
        int[] array,
        int[] temp,
        int left,
        int right)
    {
        if (left >= right)
            return;

        int middle = left + (right - left) / 2;

        MergeSortRecursive(
            array,
            temp,
            left,
            middle);

        MergeSortRecursive(
            array,
            temp,
            middle + 1,
            right);

        Merge(
            array,
            temp,
            left,
            middle,
            right);
    }

    internal static void Merge(
        int[] array,
        int[] temp,
        int left,
        int middle,
        int right)
    {
        int i = left;
        int j = middle + 1;
        int k = left;

        while (i <= middle && j <= right)
        {
            if (array[i] <= array[j])
            {
                temp[k] = array[i];
                i++;
            }
            else
            {
                temp[k] = array[j];
                j++;
            }

            k++;
        }

        while (i <= middle)
        {
            temp[k] = array[i];
            i++;
            k++;
        }

        while (j <= right)
        {
            temp[k] = array[j];
            j++;
            k++;
        }

        for (int index = left; index <= right; index++)
        {
            array[index] = temp[index];
        }
    }
}