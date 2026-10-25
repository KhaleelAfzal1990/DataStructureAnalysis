using DSALabsApi.Models;

namespace DSALabsApi.Services
{
    public class SelectionSortService
    {
        // ================= TASK 01: Descending Iterative =================
        public SortResult<int> SelectionSortDescending(int[] arr)
        {
            int[] a = (int[])arr.Clone();
            int n = a.Length;
            var result = new SortResult<int> { SortedArray = a };

            for (int i = 0; i < n - 1; i++)
            {
                int maxIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    result.Comparisons++;
                    if (a[j] > a[maxIndex]) // Descending: find max
                    {
                        maxIndex = j;
                    }
                }

                if (maxIndex != i)
                {
                    int temp = a[i];
                    a[i] = a[maxIndex];
                    a[maxIndex] = temp;
                    result.SwapsOrShifts++;
                }
                
                result.PassLogs.Add($"Pass {i + 1}: [{string.Join(", ", a)}]");
            }
            return result;
        }

        // ================= TASK 05: Recursive =================
        public void RecursiveSelectionSort(int[] arr, int start)
        {
            if (start >= arr.Length - 1) return; // Base case

            int minIndex = FindMinIndex(arr, start, arr.Length - 1);
            
            if (minIndex != start)
            {
                int temp = arr[start];
                arr[start] = arr[minIndex];
                arr[minIndex] = temp;
            }
            
            RecursiveSelectionSort(arr, start + 1);
        }

        // Helper for Recursive Selection Sort
        public int FindMinIndex(int[] arr, int start, int end)
        {
            if (start == end) return start;
            
            int minIndexInRest = FindMinIndex(arr, start + 1, end);
            
            return (arr[start] < arr[minIndexInRest]) ? start : minIndexInRest;
        }
    }
}