using DSALabsApi.Models;

namespace DSALabsApi.Services
{
    public class SortService
    {
        // ================= TASK 01 & 03: Bubble Sort =================
        // Time: O(n²) worst/average, O(n) best (with optimization)
        // Space: O(1)
        public SortResult<int> BubbleSort(int[] input, bool descending)
        {
            // Clone so we don't modify the caller's array
            int[] arr = (int[])input.Clone();
            int n = arr.Length;
            var result = new SortResult<int> { SortedArray = arr };

            if (n <= 1) return result;

            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                result.Passes++;

                for (int j = 0; j < n - i - 1; j++)
                {
                    result.Comparisons++;
                    
                    // Determine if swap is needed based on direction
                    bool shouldSwap = descending ? (arr[j] < arr[j + 1]) : (arr[j] > arr[j + 1]);

                    if (shouldSwap)
                    {
                        // Swap
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        result.Swaps++;
                        swapped = true;
                    }
                }

                // Optimization: If no elements were swapped, array is sorted
                if (!swapped) break;
            }

            return result;
        }

        // ================= TASK 02 & 03: Insertion Sort =================
        // Time: O(n²) worst/average, O(n) best
        // Space: O(1)
        public SortResult<int> InsertionSort(int[] input, bool descending)
        {
            int[] arr = (int[])input.Clone();
            int n = arr.Length;
            var result = new SortResult<int> { SortedArray = arr };

            if (n <= 1) return result;

            for (int i = 1; i < n; i++)
            {
                result.Passes++;
                int key = arr[i];
                int j = i - 1;

                // Determine comparison logic based on direction
                bool shouldShift = descending ? (arr[j] < key) : (arr[j] > key);

                while (j >= 0 && shouldShift)
                {
                    result.Comparisons++;
                    arr[j + 1] = arr[j]; // Shift element right
                    result.Shifts++;
                    j = j - 1;

                    // Re-evaluate condition for the next iteration
                    if (j >= 0)
                        shouldShift = descending ? (arr[j] < key) : (arr[j] > key);
                }

                // Only count a comparison if the while loop didn't exit because j < 0
                if (j >= 0) result.Comparisons++; 

                arr[j + 1] = key;
            }

            return result;
        }
    }
}