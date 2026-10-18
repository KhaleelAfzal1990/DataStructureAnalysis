namespace DSALabsApi.Models
{
    // Generic response wrapper — gives every endpoint a consistent shape
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ApiResponse<T> Ok(T data, string message = "Success") =>
            new() { Success = true, Message = message, Data = data };

        public static ApiResponse<T> Fail(string message) =>
            new() { Success = false, Message = message, Data = default };
    }
 // Request DTOs
    public class ValueRequest { public int Value { get; set; } }
    public class NumberRequest {public long N {get;set;}}
    public class TwoNumbersRequest {
        public long A {get; set;}
        public long B { get; set; } 
    }
     public class DigitsRequest {
         public int Digits { get; set; } 
         }

    public class AnalysisRequest { 
        public long A { get; set; } 
        public long B { get; set; } 
    }

    public class StudentInfoRequest
    {
        public string Name { get; set; } = "";
        public string RegistrationNumber { get; set; } = "";
    }

    // ---------- Request bodies ----------
    public class InsertEndRequest { public int Value { get; set; } }

    public class InsertBeginningRequest { public int Value { get; set; } }

    public class InsertPositionRequest
    {
        public int Position { get; set; }
        public int Value { get; set; }
    }

    public class UpdateRequest
    {
        public int Position { get; set; }
        public int NewValue { get; set; }
    }

    public class SearchRequest { public int Key { get; set; } }

    public class DeletePositionRequest { public int Position { get; set; } }

    // ---------- Student DTOs ----------
    public class AddStudentRequest { public int RollNumber { get; set; } }

    public class UpdateStudentRequest
    {
        public int OldRollNumber { get; set; }
        public int NewRollNumber { get; set; }
    }
    public class SortRequest
    {
        public int[] Array { get; set; } = System.Array.Empty<int>();
        public bool Descending { get; set; } = false;
    }

    // Result object to capture the stats required by the lab
    public class SortResult<T>
    {
        public T[] SortedArray { get; set; } = System.Array.Empty<T>();
        public int Comparisons { get; set; }
        public int Swaps { get; set; }       // Used by Bubble Sort
        public int Shifts { get; set; }      // Used by Insertion Sort
        public int Passes { get; set; }
    }

    public class Student
    {
        public int RollNo { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Marks { get; set; }
    }
    
    public class DeleteStudentRequest { public int RollNumber { get; set; }

    }
}