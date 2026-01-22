namespace GolfViewApartments.API.Common.Responses
{
    /// <summary>
    /// Standard API response wrapper for consistent responses
    /// </summary>
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string>? Errors { get; set; }

        public static ApiResponse SuccessResponse(string message = "Operation completed successfully")
        {
            return new ApiResponse
            {
                Success = true,
                Message = message
            };
        }

        public static ApiResponse FailureResponse(string message, List<string>? errors = null)
        {
            return new ApiResponse
            {
                Success = false,
                Message = message,
                Errors = errors
            };
        }

        public static ApiResponse FailureResponse(string message, string error)
        {
            return new ApiResponse
            {
                Success = false,
                Message = message,
                Errors = new List<string> { error }
            };
        }
    }

    /// <summary>
    /// Generic API response wrapper with data
    /// </summary>
    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Operation completed successfully")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public new static ApiResponse<T> FailureResponse(string message, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors,
                Data = default
            };
        }

        public new static ApiResponse<T> FailureResponse(string message, string error)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = new List<string> { error },
                Data = default
            };
        }
    }
}