using DSALabsApi.Models;

namespace DSALabsApi.Services
{
    public class MergeSortService
    {
        // Main MergeSort function
        public SortResult<int> MergeSort(int[] arr)
        {
            int[] a = (int[])arr.Clone();
            var result = new SortResult<int> { SortedArray = a };
            
            Sort(a, 0, a.Length - 1, result);
            return result;
        }

        private void Sort(int[] arr, int left, int right, SortResult<int> result)
        {
            if (left < right) // Base case: if left == right, array of 1 element is sorted
            {
                int mid = left + (right - left) / 2;

                // Divide
                Sort(arr, left, mid, result);
                Sort(arr, mid + 1, right, result);

                // Conquer (Merge)
                Merge(arr, left, mid, right, result);
                
                // Log the merge step
                int[] currentMerged = new int[right - left + 1];
                Array.Copy(arr, left, currentMerged, 0, currentMerged.Length);
                result.PassLogs.Add($"Merged [{left}..{right}]: [{string.Join(", ", currentMerged)}]");
            }
        }

        private void Merge(int[] arr, int left, int mid, int right, SortResult<int> result)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            // Temp arrays
            int[] L = new int[n1];
            int[] R = new int[n2];

            // Copy data to temp arrays
            for (int i = 0; i < n1; ++i) L[i] = arr[left + i];
            for (int j = 0; j < n2; ++j) R[j] = arr[mid + 1 + j];

            // Merge the temp arrays back into arr[left..right]
            int iL = 0, iR = 0, k = left;

            while (iL < n1 && iR < n2)
            {
                result.Comparisons++;
                if (L[iL] <= R[iR])
                {
                    arr[k] = L[iL];
                    iL++;
                }
                else
                {
                    arr[k] = R[iR];
                    iR++;
                }
                k++;
            }

            // Copy remaining elements
            while (iL < n1)
            {
                arr[k] = L[iL];
                iL++;
                k++;
            }

            while (iR < n2)
            {
                arr[k] = R[iR];
                iR++;
                k++;
            }
        }
    }
}