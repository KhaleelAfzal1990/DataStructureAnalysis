namespace DSALabsApi.Services
{
    public class PerformanceService
    {
        private readonly SelectionSortService _sel;
        private readonly MergeSortService _merge;

        public PerformanceService(SelectionSortService sel, MergeSortService merge)
        {
            _sel = sel;
            _merge = merge;
        }

        public object ComparePerformance(int size)
        {
            var rand = new Random(42); // Fixed seed as per lab
            int[] arr = new int[size];
            for (int i = 0; i < size; i++) arr[i] = rand.Next(1, 10000);

            // Benchmark Merge Sort
            var swMerge = System.Diagnostics.Stopwatch.StartNew();
            var mergeRes = _merge.MergeSort(arr);
            swMerge.Stop();

            // Benchmark Selection Sort
            var swSel = System.Diagnostics.Stopwatch.StartNew();
            var selRes = _sel.SelectionSortDescending(arr);
            swSel.Stop();

            return new
            {
                Size = size,
                MergeSort = new { TimeMs = swMerge.Elapsed.TotalMilliseconds, mergeRes.Comparisons },
                SelectionSort = new { TimeMs = swSel.Elapsed.TotalMilliseconds, selRes.Comparisons, selRes.SwapsOrShifts }
            };
        }
    }
}