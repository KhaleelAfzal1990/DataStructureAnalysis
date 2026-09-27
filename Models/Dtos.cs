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

    public class DeleteStudentRequest { public int RollNumber { get; set; }

    }
}