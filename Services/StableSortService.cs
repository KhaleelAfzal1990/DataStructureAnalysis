using DSALabsApi.Models;

namespace DSALabsApi.Services
{
    public class StableSortService
    {
        // ================= MERGE SORT (STABLE) =================
        public Student[] MergeSortStudents(Student[] arr, bool descending)
        {
            if (arr.Length <= 1) return arr;

            int mid = arr.Length / 2;
            Student[] left = arr.Take(mid).ToArray();
            Student[] right = arr.Skip(mid).ToArray();

            left = MergeSortStudents(left, descending);
            right = MergeSortStudents(right, descending);

            return MergeStudents(left, right, descending);
        }

        private Student[] MergeStudents(Student[] left, Student[] right, bool descending)
        {
            Student[] result = new Student[left.Length + right.Length];
            int i = 0, j = 0, k = 0;

            while (i < left.Length && j < right.Length)
            {
                // STABILITY CHECK: 
                // If marks are equal, we must pick from the LEFT array first (to keep original order).
                // In descending mode: if left.Marks >= right.Marks, pick left.
                // In ascending mode:  if left.Marks <= right.Marks, pick left.
                
                bool pickLeft;
                if (descending)
                    pickLeft = left[i].Marks >= right[j].Marks;
                else
                    pickLeft = left[i].Marks <= right[j].Marks;

                if (pickLeft)
                    result[k++] = left[i++];
                else
                    result[k++] = right[j++];
            }

            while (i < left.Length) result[k++] = left[i++];
            while (j < right.Length) result[k++] = right[j++];

            return result;
        }

        // ================= SELECTION SORT (NOT STABLE) =================
        public Student[] SelectionSortStudents(Student[] arr, bool descending)
        {
            Student[] a = (Student[])arr.Clone();
            int n = a.Length;

            for (int i = 0; i < n - 1; i++)
            {
                int targetIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    bool shouldUpdateTarget = descending ? (a[j].Marks > a[targetIndex].Marks) : (a[j].Marks < a[targetIndex].Marks);
                    if (shouldUpdateTarget)
                        targetIndex = j;
                }

                // The swap breaks stability because it can jump an element over equals
                if (targetIndex != i)
                {
                    Student temp = a[i];
                    a[i] = a[targetIndex];
                    a[targetIndex] = temp;
                }
            }
            return a;
        }
    }
}