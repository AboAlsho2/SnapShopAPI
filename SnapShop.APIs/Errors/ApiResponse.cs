namespace SnapShop.APIs.Errors
{
    public class ApiResponse
    {

        public int StatusCode { get; }
        public string? Message { get; }
        public ApiResponse(int statusCode , string? message = null) 
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageFromStatusCode(statusCode);
        }

        private string? GetDefaultMessageFromStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "You Are Unauthorized",
                404 => "Resouce Not Found",
                500 => "Server Error"
            };
        }


    }
}
