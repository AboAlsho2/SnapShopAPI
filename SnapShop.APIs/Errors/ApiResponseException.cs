namespace SnapShop.APIs.Errors
{
    public class ApiResponseException : ApiResponse
    {
        public string? Details { get; }
        public ApiResponseException( string? message = null, string? details = null) : base(500, message)
        {
            Details = details;
        }

    }
}
