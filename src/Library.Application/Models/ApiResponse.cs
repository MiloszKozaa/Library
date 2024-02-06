namespace Library.Application.ApiModels
{
    public sealed class ApiResponse
    {
        private static readonly Dictionary<string, string[]> EmptyErrorsResponse = new();
        private static readonly object? NullDataResponse = null;

        public int StatusCode { get; set; }
        public object? Data { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }

        private ApiResponse(int statusCode, object? data , Dictionary<string, string[]> errors)
        {
            StatusCode = statusCode;
            Data = data;
            Errors = errors;
        }

        public static ApiResponse Success(int statusCode, object data)
        {
            return new ApiResponse(statusCode, data, EmptyErrorsResponse);
        }

        public static ApiResponse Failure(int statusCode, string[] errors)
        {
            return new ApiResponse(statusCode, NullDataResponse, GetErrorTemplate(errors));
        }

        public static ApiResponse Failure(int statusCode, string error)
        {
            return new ApiResponse(statusCode, NullDataResponse, GetErrorTemplate(new[] { error }));
        }

        private static Dictionary<string, string[]> GetErrorTemplate(string[] errors) 
        {
            return new Dictionary<string, string[]>
            {
                {
                    "Message",
                    errors
                }
            };
        }
    }
}
