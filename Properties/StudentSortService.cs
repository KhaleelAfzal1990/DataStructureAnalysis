using DSALabsApi.Models;

namespace DSALabsApi.Services
{
    public class StudentSortService
    {
        // Bubble Sort specifically for Students (Descending Marks)
        public SortResult<Student> BubbleSortStudents(List<Student> input)
        {
            var arr = input.Select(s => new Student { RollNo = s.RollNo, Name = s.Name, Marks = s.Marks }).ToArray();
            int n = arr.Length;
            var result = new SortResult<Student> { SortedArray = arr };

            if (n <= 1) return result;

            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                result.Passes++;

                for (int j = 0; j < n - i - 1; j++)
                {
                    result.Comparisons++;
                    // Descending order: swap if current is LESS than next
                    if (arr[j].Marks < arr[j + 1].Marks)
                    {
                        var temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        result.Swaps++;
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
            return result;
        }

        // Insertion Sort specifically for Students (Descending Marks)
        public SortResult<Student> InsertionSortStudents(List<Student> input)
        {
            var arr = input.Select(s => new Student { RollNo = s.RollNo, Name = s.Name, Marks = s.Marks }).ToArray();
            int n = arr.Length;
            var result = new SortResult<Student> { SortedArray = arr };

            if (n <= 1) return result;

            for (int i = 1; i < n; i++)
            {
                result.Passes++;
                var key = arr[i];
                int j = i - 1;

                // Descending order: shift if current is LESS than key
                while (j >= 0 && arr[j].Marks < key.Marks)
                {
                    result.Comparisons++;
                    arr[j + 1] = arr[j];
                    result.Shifts++;
                    j = j - 1;
                }
                
                if (j >= 0) result.Comparisons++;
                arr[j + 1] = key;
            }
            return result;
        }
    }
}